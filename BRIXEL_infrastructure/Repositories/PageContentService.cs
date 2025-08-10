using BRIXEL_core.DTOs;
using BRIXEL_core.Interface;
using BRIXEL_core.Models;
using BRIXEL_infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BRIXEL_infrastructure.Repositories
{
    public class PageContentService : IPageContentService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<PageContentService> _logger;
        private readonly IHostEnvironment _env;

        public PageContentService(AppDbContext context, ILogger<PageContentService> logger, IHostEnvironment env)
        {
            _context = context;
            _logger = logger;
            _env = env;
        }

        public async Task<List<PageContent>> GetAllAsync()
        {
            return await _context.PageContents.ToListAsync();
        }

        public async Task<PageContent?> GetByIdAsync(int id)
        {
            return await _context.PageContents.FindAsync(id);
        }

        public async Task<bool> CreateAsync(PageContentDto dto)
        {
            try
            {
                var imageUrl = string.Empty;
                if (dto.Image != null)
                {
                    var uploadDir = Path.Combine(_env.ContentRootPath, "uploads", "pages");
                    Directory.CreateDirectory(uploadDir);

                    var fileName = $"{Guid.NewGuid()}_{dto.Image.FileName}";
                    var filePath = Path.Combine(uploadDir, fileName);

                    using var stream = new FileStream(filePath, FileMode.Create);
                    await dto.Image.CopyToAsync(stream);

                    imageUrl = $"/uploads/pages/{fileName}";
                }

                var content = new PageContent
                {
                    Page = dto.Page,
                    Section = dto.Section,
                    Title = dto.Title,
                    Content = dto.Content,
                    ImageUrl = imageUrl
                };

                _context.PageContents.Add(content);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating page content");
                return false;
            }
        }

        public async Task<bool> UpdateAsync(int id, PageContentDto dto)
        {
            try
            {
                var content = await _context.PageContents.FindAsync(id);
                if (content == null) return false;

                content.Page = dto.Page;
                content.Section = dto.Section;
                content.Title = dto.Title;
                content.Content = dto.Content;

                if (dto.Image != null)
                {
                    var uploadDir = Path.Combine(_env.ContentRootPath, "uploads", "pages");
                    Directory.CreateDirectory(uploadDir);

                    var fileName = $"{Guid.NewGuid()}_{dto.Image.FileName}";
                    var filePath = Path.Combine(uploadDir, fileName);

                    using var stream = new FileStream(filePath, FileMode.Create);
                    await dto.Image.CopyToAsync(stream);

                    content.ImageUrl = $"/uploads/pages/{fileName}";
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating page content");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var content = await _context.PageContents.FindAsync(id);
                if (content == null) return false;

                _context.PageContents.Remove(content);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting page content");
                return false;
            }
        }
    }
}
