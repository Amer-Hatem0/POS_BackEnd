using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace BRIXEL_core.DTOs
{
    public class ServiceDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? TitleAr { get; set; }
        public string? DescriptionAr { get; set; }
        public int CategoryId { get; set; }
        public decimal? PriceFrom { get; set; }
        public List<string>? Features { get; set; }
        public List<string>? Technologies { get; set; }
        public IFormFile? Icon { get; set; }
        public string? IconUrl { get; set; }
    }
}
