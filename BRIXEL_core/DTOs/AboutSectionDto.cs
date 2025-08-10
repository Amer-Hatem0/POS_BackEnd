using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 

namespace BRIXEL_core.DTOs
{
    public class AboutSectionDto
    {
        public string TitleEn { get; set; } = "";
        public string TitleAr { get; set; } = "";

        public string DescriptionEn { get; set; } = "";
        public string DescriptionAr { get; set; } = "";

        public List<string> ServicesEn { get; set; } = new();
        public List<string> ServicesAr { get; set; } = new();

        public int YearsOfExperience { get; set; }

        public IFormFile? MainImage { get; set; }
        public IFormFile? SmallImage { get; set; }
    }
}
