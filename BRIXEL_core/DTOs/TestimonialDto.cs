using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIXEL_core.DTOs
{
    public class TestimonialDto
    {
        public string? ClientName { get; set; }
        public string? ClientTitle { get; set; }
        public string? Content { get; set; }
        public IFormFile? Image { get; set; }
        public int? Rating { get; set; }
    }
}
