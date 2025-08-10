using BRIXEL_core.Interface;
using BRIXEL_core.Models;
using BRIXEL_infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIXEL_infrastructure.Repositories
{
    public class WhyChooseUsRepository : IWhyChooseUsRepository
    {
        private readonly AppDbContext _context;

        public WhyChooseUsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<WhyChooseUsSection?> GetAsync()
        {
            return await _context.WhyChooseUsSection.FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(WhyChooseUsSection section)
        {
            _context.WhyChooseUsSection.Update(section);
            await _context.SaveChangesAsync();
        }
    }

}
