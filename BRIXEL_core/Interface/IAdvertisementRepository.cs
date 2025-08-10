using BRIXEL_core.DTOs;
using BRIXEL_core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIXEL_core.Interface
{
    public interface IAdvertisementRepository
    {
        Task<List<AdvertisementResponseDto>> GetAllAsync();
        Task<AdvertisementResponseDto?> GetByIdAsync(int id);
        Task<AdvertisementResponseDto> CreateAsync(AdvertisementDto dto, string userId);
        Task<bool> UpdateAsync(int id, AdvertisementDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
