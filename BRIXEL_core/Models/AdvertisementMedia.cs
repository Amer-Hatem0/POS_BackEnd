using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIXEL_core.Models
{
    public class AdvertisementMedia
    {
        public int Id { get; set; }
        public string FileUrl { get; set; }
        public string FileType { get; set; } // e.g., image, video

        public int AdvertisementId { get; set; }
        public Advertisement Advertisement { get; set; }
    }

}
