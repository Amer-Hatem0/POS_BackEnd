using BRIXEL_core.DTOs;
using BRIXEL_core.Interface;
using BRIXEL_core.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BRIXEL_core.Models;
using BRIXEL_infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

namespace BRIXEL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _service;

        public ProjectController(IProjectService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var projects = await _service.GetAllAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var project = await _service.GetByIdAsync(id);
            return project == null ? NotFound("Project not found") : Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProjectDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return result ? Ok("Project created successfully") : BadRequest("Failed to create project");
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] ProjectUpdateDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return result ? Ok("Project updated successfully") : NotFound("Project not found");
        }

        [HttpPatch("toggle-status/{id}")]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var result = await _service.ToggleStatusAsync(id);
            return result ? Ok("Project status updated") : NotFound("Project not found");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return result ? Ok("Project deleted successfully") : NotFound("Project not found");
        }
    }
}