using BRIXEL_core.DTOs;
using BRIXEL_core.Interface;
using BRIXEL_core.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BRIXEL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyContactController : ControllerBase
    {
        private readonly ICompanyContactService _service;
        private readonly ILogger<CompanyContactController> _logger;

        public CompanyContactController(ICompanyContactService service, ILogger<CompanyContactController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _service.GetAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching company contact info");
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpPut]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(CompanyContactInfoDto dto)
        {
            try
            {
                var updated = await _service.UpdateAsync(dto);
                return updated ? Ok("Updated") : StatusCode(500, "Failed to update");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating company contact info");
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}
