using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace BRIXEL_core.DTOs
{
    public class AdvertisementDto
    {
        public string Title { get; set; }
        public string? TitleAr { get; set; }

        public string Content { get; set; }
        public string? ContentAr { get; set; }

        public string? ClientName { get; set; }
        public string? LongDescription { get; set; }
        public string? LongDescriptionAr { get; set; }

        public string? ClientUrl { get; set; }
        public string? ClientContactEmail { get; set; }
        public string? ClientContactPhone { get; set; }
        public string? ClientWebsite { get; set; }
        public string? ClientAddress { get; set; }

        public bool IsPublished { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }

        public int? CategoryId { get; set; }
        public List<IFormFile>? MediaFiles { get; set; }
    }
}
