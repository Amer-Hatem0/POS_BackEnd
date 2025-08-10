using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIXEL_core.Models
{
    public class PageContent
    {
        public int Id { get; set; }
        public string Page { get; set; } // Home, About, Footer
        public string Section { get; set; } // Hero, Vision, Features
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ImageUrl { get; set; }
    }

}
