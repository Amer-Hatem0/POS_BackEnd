using BRIXEL_core.DTOs;
using BRIXEL_core.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIXEL_core.Interface
{
    public interface ILoginService
    {
        Task<AuthResponseDto?> LoginAsync(LoginDto dto);
        Task<string> CreateUserAsync(RegisterDto dto);
        Task<List<string>> GetAllRolesAsync();
        Task<IdentityResult> UpdateUserAsync(string userId, RegisterDto dto);
        Task<IdentityResult> DeleteUserAsync(string userId);
        Task<IdentityResult> ResetPasswordAsync(ResetPasswordDto dto);
        Task<List<UserDto>> GetAllUsersAsync();

    }
}
