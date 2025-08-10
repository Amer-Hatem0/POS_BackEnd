using BRIXEL_core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIXEL_core.Interface
{
    public interface ICompanyContactService
    {
        Task<CompanyContactInfoDto?> GetAsync();
        Task<bool> UpdateAsync(CompanyContactInfoDto dto);
    }
}
