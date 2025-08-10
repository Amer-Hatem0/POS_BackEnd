using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIXEL_core.DTOs
{
    public class PageContentDto
    {
        public string Page { get; set; }      // Home, About, Footer, etc.
        public string Section { get; set; }   // Hero, Vision, Features, etc.
        public string Title { get; set; }
        public string Content { get; set; }
        public IFormFile? Image { get; set; }
    }
}
