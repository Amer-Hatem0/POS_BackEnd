using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIXEL_core.DTOs
{
    public class WhyChooseUsDto
    {
        public string MainTitleEn { get; set; }
        public string MainTitleAr { get; set; }

        public string SubtitleEn { get; set; }
        public string SubtitleAr { get; set; }

        public string HighlightTitleEn { get; set; }
        public string HighlightTitleAr { get; set; }

        public string HighlightDescriptionEn { get; set; }
        public string HighlightDescriptionAr { get; set; }

        public List<string> BulletPointsEn { get; set; }
        public List<string> BulletPointsAr { get; set; }

        public IFormFile? Image { get; set; }
    }


}
