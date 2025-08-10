using BRIXEL_core.DTOs;
using BRIXEL_core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIXEL_core.Interface
{
    public interface ITestimonialService
    {
        Task<List<Testimonial>> GetAllAsync();
        Task<List<Testimonial>> GetApprovedAsync();
        Task<bool> CreateAsync(TestimonialDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> ApproveAsync(int id);
        Task<bool> ToggleVisibilityAsync(int id);
    }
}
