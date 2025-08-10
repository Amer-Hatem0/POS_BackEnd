using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIXEL_core.Models
{
    public class Testimonial
    {
        public int Id { get; set; }
        public string? ClientName { get; set; }
        public string? ClientTitle { get; set; }
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }
        public int? Rating { get; set; }
        public bool IsApproved { get; set; } = false;
        public bool IsVisible { get; set; } = true;
        public DateTime CreatedAt { get; set; }
    }


}
