using BRIXEL_core.DTOs;
using BRIXEL_core.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BRIXEL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AboutSectionController : ControllerBase
    {
        private readonly IAboutSectionRepository _repo;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<AboutSectionController> _logger;

        public AboutSectionController(IAboutSectionRepository repo, IWebHostEnvironment env, ILogger<AboutSectionController> logger)
        {
            _repo = repo;
            _env = env;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var data = await _repo.GetAsync();
                return data != null ? Ok(data) : NotFound("About section not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching about section.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] AboutSectionDto dto)
        {
            try
            {
                var section = await _repo.GetAsync();
                if (section == null) return NotFound("Section not found.");

                section.TitleEn = dto.TitleEn?.Trim() ?? "";
                section.TitleAr = dto.TitleAr?.Trim() ?? "";
                section.DescriptionEn = dto.DescriptionEn?.Trim() ?? "";
                section.DescriptionAr = dto.DescriptionAr?.Trim() ?? "";
                section.ServicesEn = dto.ServicesEn?.Where(s => !string.IsNullOrWhiteSpace(s)).ToList() ?? new();
                section.ServicesAr = dto.ServicesAr?.Where(s => !string.IsNullOrWhiteSpace(s)).ToList() ?? new();
                section.YearsOfExperience = dto.YearsOfExperience;

                if (dto.MainImage != null && dto.MainImage.Length > 0)
                {
                    var fileName = $"main_{DateTime.UtcNow.Ticks}{Path.GetExtension(dto.MainImage.FileName)}";
                    var path = Path.Combine(_env.WebRootPath, "uploads/about", fileName);
                    Directory.CreateDirectory(Path.GetDirectoryName(path)!);
                    using var stream = new FileStream(path, FileMode.Create);
                    await dto.MainImage.CopyToAsync(stream);
                    section.MainImageUrl = $"/uploads/about/{fileName}";
                }

                if (dto.SmallImage != null && dto.SmallImage.Length > 0)
                {
                    var fileName = $"small_{DateTime.UtcNow.Ticks}{Path.GetExtension(dto.SmallImage.FileName)}";
                    var path = Path.Combine(_env.WebRootPath, "uploads/about", fileName);
                    Directory.CreateDirectory(Path.GetDirectoryName(path)!);
                    using var stream = new FileStream(path, FileMode.Create);
                    await dto.SmallImage.CopyToAsync(stream);
                    section.SmallImageUrl = $"/uploads/about/{fileName}";
                }

                await _repo.UpdateAsync(section);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating about section.");
                return StatusCode(500, "Failed to update section.");
            }
        }
    }
}
