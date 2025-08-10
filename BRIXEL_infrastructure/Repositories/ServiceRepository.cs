using BRIXEL_core.DTOs;
using BRIXEL_core.Interface;
using BRIXEL_core.Models;
using BRIXEL_infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace BRIXEL_infrastructure.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ServiceRepository> _logger;

        public ServiceRepository(AppDbContext context, ILogger<ServiceRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Service>> GetAllAsync()
        {
            return await _context.Services.Include(s => s.Category).ToListAsync();
        }

        public async Task<Service?> GetByIdAsync(int id)
        {
            return await _context.Services.Include(s => s.Category).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Service> CreateAsync(ServiceDto dto)
        {
            var service = new Service
            {
                Title = dto.Title,
                Description = dto.Description,
                TitleAr = dto.TitleAr,
                DescriptionAr = dto.DescriptionAr,
                IconUrl = dto.IconUrl,
                CategoryId = dto.CategoryId,
                CreatedAt = DateTime.UtcNow,
                PriceFrom = dto.PriceFrom,
                FeaturesJson = dto.Features == null ? null : JsonSerializer.Serialize(dto.Features),
                TechnologiesJson = dto.Technologies == null ? null : JsonSerializer.Serialize(dto.Technologies)
            };

            _context.Services.Add(service);
            await _context.SaveChangesAsync();
            return service;
        }

        public async Task<Service?> UpdateAsync(int id, ServiceDto dto)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null) return null;

            service.Title = dto.Title ?? service.Title;
            service.Description = dto.Description ?? service.Description;
            service.TitleAr = dto.TitleAr ?? service.TitleAr;
            service.DescriptionAr = dto.DescriptionAr ?? service.DescriptionAr;
            if (!string.IsNullOrWhiteSpace(dto.IconUrl)) service.IconUrl = dto.IconUrl;
            service.CategoryId = dto.CategoryId != 0 ? dto.CategoryId : service.CategoryId;
            service.PriceFrom = dto.PriceFrom ?? service.PriceFrom;
            if (dto.Features != null) service.FeaturesJson = JsonSerializer.Serialize(dto.Features);
            if (dto.Technologies != null) service.TechnologiesJson = JsonSerializer.Serialize(dto.Technologies);

            await _context.SaveChangesAsync();
            return service;
        }

        public async Task<bool> ToggleVisibilityAsync(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null) return false;
            service.IsVisible = !service.IsVisible;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null) return false;
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
