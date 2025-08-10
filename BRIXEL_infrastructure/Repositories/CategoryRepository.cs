using BRIXEL_core.DTOs;
using BRIXEL_core.Interface;
using BRIXEL_core.Models;
using BRIXEL_core.Models.DTOs;
using BRIXEL_infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BRIXEL_infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CategoryRepository> _logger;

        public CategoryRepository(AppDbContext context, ILogger<CategoryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            try
            {
                return await _context.Categories
                    .Include(c => c.SubCategories)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync()");
                throw;
            }
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Categories
                    .Include(c => c.SubCategories)
                    .FirstOrDefaultAsync(c => c.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync(id={id})");
                throw;
            }
        }

        public async Task<Category> CreateAsync(CategoryDto dto)
        {
            try
            {
                var category = new Category
                {
                    Name = dto.Name,
                    NameAr = dto.NameAr,
                    Description = dto.Description,
                    DescriptionAr = dto.DescriptionAr,
                    ImageUrl = dto.ImageUrl,
                    ParentCategoryId = dto.ParentCategoryId
                };


                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return category;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CreateAsync()");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(int id, CategoryDto dto)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (category == null) return false;

                category.Name = dto.Name;
                category.NameAr = dto.NameAr;
                category.Description = dto.Description;
                category.DescriptionAr = dto.DescriptionAr;
                category.ImageUrl = dto.ImageUrl;
                category.ParentCategoryId = dto.ParentCategoryId;


                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in UpdateAsync(id={id})");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (category == null) return false;

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in DeleteAsync(id={id})");
                throw;
            }
        }
    }
}
