using BRIXEL_core.DTOs;
using BRIXEL_core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BRIXEL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WhyChooseUsController : ControllerBase
    {
        private readonly IWhyChooseUsRepository _repo;
        private readonly IWebHostEnvironment _env;

        public WhyChooseUsController(IWhyChooseUsRepository repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var section = await _repo.GetAsync();
            return Ok(section);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] WhyChooseUsDto dto)
        {
            var section = await _repo.GetAsync();
            if (section == null) return NotFound();

            section.MainTitleEn = dto.MainTitleEn;
            section.MainTitleAr = dto.MainTitleAr;

            section.SubtitleEn = dto.SubtitleEn;
            section.SubtitleAr = dto.SubtitleAr;

            section.HighlightTitleEn = dto.HighlightTitleEn;
            section.HighlightTitleAr = dto.HighlightTitleAr;

            section.HighlightDescriptionEn = dto.HighlightDescriptionEn;
            section.HighlightDescriptionAr = dto.HighlightDescriptionAr;

            section.BulletPointsEn = dto.BulletPointsEn;
            section.BulletPointsAr = dto.BulletPointsAr;

            if (dto.Image != null)
            {
                var fileName = $"whychoose_{DateTime.Now.Ticks}{Path.GetExtension(dto.Image.FileName)}";
                var path = Path.Combine(_env.WebRootPath, "uploads/about", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(path)!);
                using var stream = new FileStream(path, FileMode.Create);
                await dto.Image.CopyToAsync(stream);
                section.ImageUrl = $"/uploads/about/{fileName}";
            }

            await _repo.UpdateAsync(section);
            return NoContent();
        }
    }
}
