using BRIXEL_core.DTOs;
using BRIXEL_core.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BRIXEL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FAQController : ControllerBase
    {
        private readonly IFAQService _faqService;

        public FAQController(IFAQService faqService)
        {
            _faqService = faqService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _faqService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _faqService.GetByIdAsync(id);
            return result is null ? NotFound("FAQ not found") : Ok(result);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] FAQDto dto)
        {
            var success = await _faqService.CreateAsync(dto);
            return success ? Ok("Created") : StatusCode(500, "Error occurred");
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] FAQDto dto)
        {
            var success = await _faqService.UpdateAsync(id, dto);
            return success ? Ok("Updated") : NotFound("FAQ not found");
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _faqService.DeleteAsync(id);
            return success ? Ok("Deleted") : NotFound("FAQ not found");
        }

        [HttpPut("{id}/toggle")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var success = await _faqService.ToggleStatusAsync(id);
            return success ? Ok("Toggled") : NotFound("FAQ not found");
        }
    }
}
