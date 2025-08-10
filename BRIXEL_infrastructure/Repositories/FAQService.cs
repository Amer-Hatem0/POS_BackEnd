using BRIXEL_core.DTOs;
using BRIXEL_core.Interface;
using BRIXEL_core.Models;
using BRIXEL_infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BRIXEL_infrastructure.Repositories
{
    public class FAQService : IFAQService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<FAQService> _logger;

        public FAQService(AppDbContext context, ILogger<FAQService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<FAQ>> GetAllAsync()
        {
            return await _context.FAQs.OrderBy(f => f.DisplayOrder).ToListAsync();
        }

        public async Task<FAQ?> GetByIdAsync(int id)
        {
            return await _context.FAQs.FindAsync(id);
        }

        public async Task<bool> CreateAsync(FAQDto dto)
        {
            try
            {
                var faq = new FAQ
                {
                    QuestionAr = dto.QuestionAr,
                    AnswerAr = dto.AnswerAr,
                    QuestionEn = dto.QuestionEn,
                    AnswerEn = dto.AnswerEn,
                    DisplayOrder = dto.DisplayOrder
                };

                _context.FAQs.Add(faq);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating FAQ");
                return false;
            }
        }

        public async Task<bool> UpdateAsync(int id, FAQDto dto)
        {
            try
            {
                var faq = await _context.FAQs.FindAsync(id);
                if (faq == null) return false;

                faq.QuestionAr = dto.QuestionAr;
                faq.AnswerAr = dto.AnswerAr;
                faq.QuestionEn = dto.QuestionEn;
                faq.AnswerEn = dto.AnswerEn;
                faq.DisplayOrder = dto.DisplayOrder;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating FAQ");
                return false;
            }
        }

        public async Task<bool> ToggleStatusAsync(int id)
        {
            try
            {
                var faq = await _context.FAQs.FindAsync(id);
                if (faq == null) return false;

                faq.IsActive = !faq.IsActive;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error toggling FAQ status");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var faq = await _context.FAQs.FindAsync(id);
                if (faq == null) return false;

                _context.FAQs.Remove(faq);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting FAQ");
                return false;
            }
        }
    }
}
