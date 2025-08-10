using BRIXEL_core.DTOs;
using BRIXEL_core.Interface;
using BRIXEL_core.Models;
using BRIXEL_core.Models.DTOs;
using BRIXEL_infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BRIXEL_infrastructure.Repositories
{
    public class CompanyContactService : ICompanyContactService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CompanyContactService> _logger;

        public CompanyContactService(AppDbContext context, ILogger<CompanyContactService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<CompanyContactInfoDto?> GetAsync()
        {
            try
            {
                var data = await _context.CompanyContactInfos.FirstOrDefaultAsync();
                if (data == null) return null;

                return new CompanyContactInfoDto
                {
                    PhoneNumber1 = data.PhoneNumber1,
                    PhoneNumber2 = data.PhoneNumber2,
                    Email = data.Email,
                    Address = data.Address,
                    FacebookUrl = data.FacebookUrl,
                    InstagramUrl = data.InstagramUrl,
                    LinkedInUrl = data.LinkedInUrl,
                    TwitterUrl = data.TwitterUrl,
                    WhatsAppNumber = data.WhatsAppNumber,
                        AddressAr = data.AddressAr,
                    DescriptionAr = data.DescriptionAr,
                    TaglineAr = data.TaglineAr,
                       Description = data.Description,
                    Tagline = data.Tagline
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving company contact info");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(CompanyContactInfoDto dto)
        {
            try
            {
                var entity = await _context.CompanyContactInfos.FirstOrDefaultAsync();
                if (entity == null)
                {
                    entity = new CompanyContactInfo();
                    _context.CompanyContactInfos.Add(entity);
                }

                entity.PhoneNumber1 = dto.PhoneNumber1;
                entity.PhoneNumber2 = dto.PhoneNumber2;
                entity.Email = dto.Email;
                entity.Address = dto.Address;
                entity.FacebookUrl = dto.FacebookUrl;
                entity.InstagramUrl = dto.InstagramUrl;
                entity.LinkedInUrl = dto.LinkedInUrl;
                entity.TwitterUrl = dto.TwitterUrl;
                entity.WhatsAppNumber = dto.WhatsAppNumber;
                entity.LastUpdated = DateTime.UtcNow;
                entity.AddressAr = dto.AddressAr;
                entity.DescriptionAr = dto.DescriptionAr;
                entity.TaglineAr = dto.TaglineAr;
                entity.Description = dto.Description;
                entity.Tagline = dto.Tagline;


                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating company contact info");
                throw;
            }
        }
    }
}
