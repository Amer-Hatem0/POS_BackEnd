using BRIXEL_core.DTOs;
using BRIXEL_core.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BRIXEL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestimonialController : ControllerBase
    {
        private readonly ITestimonialService _testimonialService;

        public TestimonialController(ITestimonialService testimonialService)
        {
            _testimonialService = testimonialService;
        }

        [HttpGet]
   
        public async Task<IActionResult> GetAll()
        {
            var testimonials = await _testimonialService.GetAllAsync();
            return Ok(testimonials);
        }

        [HttpGet("public")]
        [AllowAnonymous]
        public async Task<IActionResult> GetApproved()
        {
            var testimonials = await _testimonialService.GetApprovedAsync();
            return Ok(testimonials);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromForm] TestimonialDto dto)
        {
            var result = await _testimonialService.CreateAsync(dto);
            return result ? Ok("Created") : BadRequest("Failed to create");
        }

        [HttpDelete("{id}")]
 
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _testimonialService.DeleteAsync(id);
            return result ? Ok("Deleted") : NotFound("Not found");
        }

        [HttpPatch("{id}/approve")]
     
        public async Task<IActionResult> Approve(int id)
        {
            var result = await _testimonialService.ApproveAsync(id);
            return result ? Ok("Approved") : NotFound("Not found");
        }

        [HttpPatch("{id}/toggle-visibility")]
 
        public async Task<IActionResult> ToggleVisibility(int id)
        {
            var result = await _testimonialService.ToggleVisibilityAsync(id);
            return result ? Ok("Visibility toggled") : NotFound("Not found");
        }
    }
}
