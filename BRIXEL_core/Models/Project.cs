using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIXEL_core.Models
{
    public class Project
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string? TitleAr { get; set; }

        public string Description { get; set; }
        public string? DescriptionAr { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsActive { get; set; } = true;
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string? Client { get; set; }
        public string? Duration { get; set; }
        public string? Technologies { get; set; }
        public string? Features { get; set; }
        public string? LiveDemoUrl { get; set; }
        public string? SourceCodeUrl { get; set; }
        public List<ProjectImage> ProjectImages { get; set; } = new List<ProjectImage>();
    }
}