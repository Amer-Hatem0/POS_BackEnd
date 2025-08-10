using BRIXEL_core.DTOs;
using BRIXEL_core.Interface;
using BRIXEL_core.Models.DTOs;
using BRIXEL_infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BRIXEL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = "Admin")]
    public class AuthController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public AuthController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _loginService.LoginAsync(loginDto);
            if (result == null)
                return Unauthorized("Invalid email or password");

            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] RegisterDto dto)
        {
            var result = await _loginService.CreateUserAsync(dto);

            if (result.StartsWith("Failed"))
                return BadRequest(result);

            return Ok(result);
        }


        [HttpGet("roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _loginService.GetAllRolesAsync();
            return Ok(roles);
        }

        [HttpPut("update/{userId}")]
        public async Task<IActionResult> UpdateUser(string userId, [FromBody] RegisterDto dto)
        {
            var result = await _loginService.UpdateUserAsync(userId, dto);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("User updated successfully.");
        }

        [HttpDelete("delete/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var result = await _loginService.DeleteUserAsync(userId);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("User deleted successfully.");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            var result = await _loginService.ResetPasswordAsync(dto);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Password reset successfully.");
        }
        [HttpGet("users")]
        //[Authorize(Roles = "Admin")] 
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _loginService.GetAllUsersAsync();
            return Ok(users);
        }


    }
}
