using BRIXEL_core.DTOs;
using BRIXEL_core.Interface;
using BRIXEL_core.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BRIXEL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceRepository _repo;
        private readonly ILogger<ServiceController> _logger;

        public ServiceController(IServiceRepository repo, ILogger<ServiceController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var services = await _repo.GetAllAsync();
                var result = services.Select(ServiceViewModel.FromEntity).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving services");
                return StatusCode(500, "Something went wrong.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var service = await _repo.GetByIdAsync(id);
                if (service == null) return NotFound();
                return Ok(ServiceViewModel.FromEntity(service));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving service with id={id}");
                return StatusCode(500, "Something went wrong.");
            }
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ServiceDto dto)
        {
            try
            {
                string iconPath = null;
                if (dto.Icon != null)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(dto.Icon.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
                    using var stream = new FileStream(filePath, FileMode.Create);
                    await dto.Icon.CopyToAsync(stream);
                    iconPath = $"/uploads/{fileName}";
                }

                dto.IconUrl = iconPath;
                var created = await _repo.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, ServiceViewModel.FromEntity(created));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating service with icon");
                return StatusCode(500, "Unable to create service.");
            }
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(int id, [FromForm] ServiceDto dto)
        {
            try
            {
                string iconPath = null;
                if (dto.Icon != null)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(dto.Icon.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
                    using var stream = new FileStream(filePath, FileMode.Create);
                    await dto.Icon.CopyToAsync(stream);
                    iconPath = $"/uploads/{fileName}";
                }

                dto.IconUrl = iconPath;
                var updated = await _repo.UpdateAsync(id, dto);
                if (updated == null) return NotFound();
                return Ok(ServiceViewModel.FromEntity(updated));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating service with id={id}");
                return StatusCode(500, "Unable to update service.");
            }
        }

        [HttpPut("toggle/{id}")]
        public async Task<IActionResult> ToggleVisibility(int id)
        {
            var success = await _repo.ToggleVisibilityAsync(id);
            if (!success) return NotFound("Service not found.");
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _repo.DeleteAsync(id);
                return deleted ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting service with id={id}");
                return StatusCode(500, "Unable to delete service.");
            }
        }
    }
}
