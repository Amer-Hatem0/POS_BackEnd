using BRIXEL_core.Interface;
using BRIXEL_core.Models;
using BRIXEL_infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BRIXEL_infrastructure.Repositories
{
    public class AboutSectionRepository : IAboutSectionRepository
    {
        private readonly AppDbContext _context;

        public AboutSectionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AboutSection?> GetAsync()
        {
            return await _context.AboutSection.FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(AboutSection section)
        {
            _context.AboutSection.Update(section);
            await _context.SaveChangesAsync();
        }
    }
}
