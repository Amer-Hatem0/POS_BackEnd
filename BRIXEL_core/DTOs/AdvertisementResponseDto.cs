using System;
using System.Collections.Generic;

namespace BRIXEL_core.DTOs
{
    public class AdvertisementResponseDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string? TitleAr { get; set; }

        public string Content { get; set; } = string.Empty;
        public string? ContentAr { get; set; }

        public string? ClientName { get; set; }
        public string? LongDescription { get; set; }
        public string? LongDescriptionAr { get; set; }

        public string? ClientUrl { get; set; }
        public string? ClientContactEmail { get; set; }
        public string? ClientContactPhone { get; set; }
        public string? ClientWebsite { get; set; }
        public string? ClientAddress { get; set; }

        public string? ImageUrl { get; set; }
        public bool IsPublished { get; set; }

        public DateTime? ExpirationDate { get; set; }
        public string? CreatedById { get; set; }
        public DateTime CreatedAt { get; set; }

        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public List<string> MediaUrls { get; set; } = new();
    }
}
