using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIXEL_core.DTOs
{
    public class CategoryDto
    {
        public string Name { get; set; }
        public string NameAr { get; set; }  
        public string? Description { get; set; }
        public string? DescriptionAr { get; set; }  
        public int? ParentCategoryId { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
    }

}
