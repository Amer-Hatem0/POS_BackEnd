using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIXEL_core.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; } 
        public string? Description { get; set; }
        public string? DescriptionAr { get; set; }  
        public string? ImageUrl { get; set; }

        public int? ParentCategoryId { get; set; }
        public Category? ParentCategory { get; set; }
        public List<Category> SubCategories { get; set; } = new();
        public List<Service> Services { get; set; } = new();
        public List<Project> Projects { get; set; } = new();
    }


}
