using BRIXEL_core.DTOs;
using BRIXEL_core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BRIXEL_core.Interface
{
    public interface IServiceRepository
    {
        Task<List<Service>> GetAllAsync();
        Task<Service?> GetByIdAsync(int id);
        Task<Service> CreateAsync(ServiceDto dto);
        Task<Service?> UpdateAsync(int id, ServiceDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> ToggleVisibilityAsync(int id);
    }
}
