using BRIXEL_core.DTOs;
using BRIXEL_core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIXEL_core.Interface
{
    public interface IPageContentService
    {
        Task<List<PageContent>> GetAllAsync();
        Task<PageContent?> GetByIdAsync(int id);
        Task<bool> CreateAsync(PageContentDto dto);
        Task<bool> UpdateAsync(int id, PageContentDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
