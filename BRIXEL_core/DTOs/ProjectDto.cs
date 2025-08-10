using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIXEL_core.DTOs
{
    public class ProjectDto
    {
        public string Title { get; set; }
        public string? TitleAr { get; set; }

        public string Description { get; set; }
        public string? DescriptionAr { get; set; }

        public int CategoryId { get; set; }
        public List<IFormFile>? Images { get; set; }

        public bool IsActive { get; set; } = true;

        public string? Client { get; set; }
        public string? Duration { get; set; }
        public string? Technologies { get; set; }
        public string? Features { get; set; }
        public string? LiveDemoUrl { get; set; }
        public string? SourceCodeUrl { get; set; }
    }
}