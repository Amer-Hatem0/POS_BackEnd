using BRIXEL_core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace BRIXEL_core.DTOs
{
    public class ServiceViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? TitleAr { get; set; }
        public string? DescriptionAr { get; set; }
        public string? IconUrl { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public bool IsVisible { get; set; }
        public decimal? PriceFrom { get; set; }
        public List<string>? Features { get; set; }
        public List<string>? Technologies { get; set; }

        public static ServiceViewModel FromEntity(Service s)
        {
            return new ServiceViewModel
            {
                Id = s.Id,
                Title = s.Title,
                Description = s.Description,
                TitleAr = s.TitleAr,
                DescriptionAr = s.DescriptionAr,
                IconUrl = s.IconUrl,
                CategoryId = s.CategoryId,
                CategoryName = s.Category?.Name ?? "Unknown",
                IsVisible = s.IsVisible,
                PriceFrom = s.PriceFrom,
                Features = string.IsNullOrWhiteSpace(s.FeaturesJson) ? new List<string>() : JsonSerializer.Deserialize<List<string>>(s.FeaturesJson),
                Technologies = string.IsNullOrWhiteSpace(s.TechnologiesJson) ? new List<string>() : JsonSerializer.Deserialize<List<string>>(s.TechnologiesJson)
            };
        }
    }
}
