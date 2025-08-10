using BRIXEL_core.DTOs;
using BRIXEL_core.Interface;
using BRIXEL_core.Models;
using BRIXEL_core.Models.DTOs;
using BRIXEL_infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

namespace BRIXEL_infrastructure.Repositories
{
    public class ProjectService : IProjectService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ProjectService> _logger;
        private readonly IHostEnvironment _env;

        public ProjectService(AppDbContext context, ILogger<ProjectService> logger, IHostEnvironment env)
        {
            _context = context;
            _logger = logger;
            _env = env;
        }

        public async Task<List<ProjectResponseDto>> GetAllAsync()
        {
            try
            {
                var projects = await _context.Projects
                    .Include(p => p.Category)
                    .Include(p => p.ProjectImages)
                    .OrderByDescending(p => p.CreatedAt)
                    .ToListAsync();

                return projects.Select(p => new ProjectResponseDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    TitleAr = p.TitleAr,
                    Description = p.Description,
                    CategoryNameAr = p.Category.NameAr,

                    DescriptionAr = p.DescriptionAr,
                    ImageUrls = p.ProjectImages.Select(i => i.ImageUrl).ToList(),
                    CreatedAt = p.CreatedAt,
                    IsActive = p.IsActive,
                    CategoryName = p.Category.Name,
                    Client = p.Client,
                    Duration = p.Duration,
                    Technologies = string.IsNullOrWhiteSpace(p.Technologies) ? null : p.Technologies.Split(',').ToList(),
                    Features = string.IsNullOrWhiteSpace(p.Features) ? null : p.Features.Split(',').ToList(),
                    LiveDemoUrl = p.LiveDemoUrl,
                    SourceCodeUrl = p.SourceCodeUrl
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving projects");
                return new List<ProjectResponseDto>();
            }
        }

        public async Task<ProjectResponseDto?> GetByIdAsync(int id)
        {
            try
            {
                var p = await _context.Projects
                    .Include(p => p.Category)
                    .Include(p => p.ProjectImages)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (p == null) return null;

                return new ProjectResponseDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    TitleAr = p.TitleAr,
                    CategoryNameAr = p.Category.NameAr,
                    Description = p.Description,
                    DescriptionAr = p.DescriptionAr,
                    ImageUrls = p.ProjectImages.Select(i => i.ImageUrl).ToList(),
                    CreatedAt = p.CreatedAt,
                    IsActive = p.IsActive,
                    CategoryName = p.Category.Name,
                    Client = p.Client,
                    Duration = p.Duration,
                    Technologies = string.IsNullOrWhiteSpace(p.Technologies) ? null : p.Technologies.Split(',').ToList(),
                    Features = string.IsNullOrWhiteSpace(p.Features) ? null : p.Features.Split(',').ToList(),
                    LiveDemoUrl = p.LiveDemoUrl,
                    SourceCodeUrl = p.SourceCodeUrl
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving project by id");
                return null;
            }
        }

        public async Task<bool> CreateAsync(ProjectDto dto)
        {
            try
            {
                if (!_context.Categories.Any(c => c.Id == dto.CategoryId)) return false;

                var project = new Project
                {
                    Title = dto.Title,
                    TitleAr = dto.TitleAr,
                    Description = dto.Description,
                    DescriptionAr = dto.DescriptionAr,
                    CategoryId = dto.CategoryId,
                    IsActive = dto.IsActive,
                    CreatedAt = DateTime.UtcNow,
                    Client = dto.Client,
                    Duration = dto.Duration,
                    Technologies = dto.Technologies,
                    Features = dto.Features,
                    LiveDemoUrl = dto.LiveDemoUrl,
                    SourceCodeUrl = dto.SourceCodeUrl
                };

                if (dto.Images != null && dto.Images.Any())
                {
                    var uploadDir = Path.Combine(_env.ContentRootPath, "wwwroot", "uploads", "projects");
                    Directory.CreateDirectory(uploadDir);

                    foreach (var image in dto.Images)
                    {
                        var fileName = $"{Guid.NewGuid()}_{image.FileName}";
                        var filePath = Path.Combine(uploadDir, fileName);

                        using var stream = new FileStream(filePath, FileMode.Create);
                        await image.CopyToAsync(stream);

                        project.ProjectImages.Add(new ProjectImage { ImageUrl = $"/uploads/projects/{fileName}" });
                    }
                }

                _context.Projects.Add(project);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating project");
                return false;
            }
        }

        public async Task<bool> UpdateAsync(int id, ProjectUpdateDto dto)
        {
            try
            {
                var project = await _context.Projects.Include(p => p.ProjectImages).FirstOrDefaultAsync(p => p.Id == id);
                if (project == null) return false;

                if (dto.Title != null) project.Title = dto.Title;
                if (dto.TitleAr != null) project.TitleAr = dto.TitleAr;
                if (dto.Description != null) project.Description = dto.Description;
                if (dto.DescriptionAr != null) project.DescriptionAr = dto.DescriptionAr;
                if (dto.CategoryId != null) project.CategoryId = dto.CategoryId.Value;
                if (dto.IsActive != null) project.IsActive = dto.IsActive.Value;
                if (dto.Client != null) project.Client = dto.Client;
                if (dto.Duration != null) project.Duration = dto.Duration;
                if (dto.Technologies != null) project.Technologies = dto.Technologies;
                if (dto.Features != null) project.Features = dto.Features;
                if (dto.LiveDemoUrl != null) project.LiveDemoUrl = dto.LiveDemoUrl;
                if (dto.SourceCodeUrl != null) project.SourceCodeUrl = dto.SourceCodeUrl;

                if (dto.Images != null && dto.Images.Any())
                {
                    var uploadDir = Path.Combine(_env.ContentRootPath, "wwwroot", "uploads", "projects");
                    Directory.CreateDirectory(uploadDir);

                    foreach (var oldImage in project.ProjectImages.ToList())
                    {
                        var oldImagePath = Path.Combine(_env.ContentRootPath, "wwwroot", oldImage.ImageUrl.TrimStart('/'));
                        if (File.Exists(oldImagePath)) File.Delete(oldImagePath);
                        _context.ProjectImages.Remove(oldImage);
                    }

                    foreach (var image in dto.Images)
                    {
                        var fileName = $"{Guid.NewGuid()}_{image.FileName}";
                        var filePath = Path.Combine(uploadDir, fileName);

                        using var stream = new FileStream(filePath, FileMode.Create);
                        await image.CopyToAsync(stream);

                        project.ProjectImages.Add(new ProjectImage { ImageUrl = $"/uploads/projects/{fileName}" });
                    }
                }

                _context.Projects.Update(project);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating project");
                return false;
            }
        }

        public async Task<bool> ToggleStatusAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return false;

            project.IsActive = !project.IsActive;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var project = await _context.Projects.Include(p => p.ProjectImages).FirstOrDefaultAsync(p => p.Id == id);
                if (project == null) return false;

                foreach (var image in project.ProjectImages)
                {
                    var imagePath = Path.Combine(_env.ContentRootPath, "wwwroot", image.ImageUrl.TrimStart('/'));
                    if (File.Exists(imagePath)) File.Delete(imagePath);
                }

                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting project");
                return false;
            }
        }
    }
}