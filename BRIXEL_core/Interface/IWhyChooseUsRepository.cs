using BRIXEL_core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIXEL_core.Interface
{
    public interface IWhyChooseUsRepository
    {
        Task<WhyChooseUsSection?> GetAsync();
        Task UpdateAsync(WhyChooseUsSection section);
    }

}
