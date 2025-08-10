using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BRIXEL_core.Models
{
    public class MediaFile
    {
        public int Id { get; set; }
        public string FilePath { get; set; } = null!;

       
        public int AdvertisementId { get; set; }
        [JsonIgnore]
        public Advertisement Advertisement { get; set; } = null!;



    }
}
