using BRIXEL_core.DTOs;
using BRIXEL_core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIXEL_core.Interface
{
    public interface IFAQService
    {
        Task<List<FAQ>> GetAllAsync();
        Task<FAQ?> GetByIdAsync(int id);
        Task<bool> CreateAsync(FAQDto dto);
        Task<bool> UpdateAsync(int id, FAQDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> ToggleStatusAsync(int id);
    }
}
