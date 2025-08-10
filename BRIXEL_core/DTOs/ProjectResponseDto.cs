using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIXEL_core.DTOs
{
    public class ProjectResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? TitleAr { get; set; }

        public string Description { get; set; }
        public string? DescriptionAr { get; set; }

        public List<string>? ImageUrls { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }

        public string CategoryName { get; set; }
        public string CategoryNameAr { get; set; }

        public string? Client { get; set; }
        public string? Duration { get; set; }
        public List<string>? Technologies { get; set; }
        public List<string>? Features { get; set; }
        public string? LiveDemoUrl { get; set; }
        public string? SourceCodeUrl { get; set; }
    }
}