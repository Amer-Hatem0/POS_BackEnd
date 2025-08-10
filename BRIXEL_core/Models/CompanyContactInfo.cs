using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIXEL_core.Models
{
    public class CompanyContactInfo
    {
        public int Id { get; set; }
        public string? PhoneNumber1 { get; set; }
        public string? PhoneNumber2 { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? FacebookUrl { get; set; }
        public string? InstagramUrl { get; set; }
        public string? LinkedInUrl { get; set; }
        public string? TwitterUrl { get; set; }
        public string? WhatsAppNumber { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string? AddressAr { get; set; }
        public string? DescriptionAr { get; set; }
        public string? TaglineAr { get; set; }
        public string? Description { get; set; }
        public string? Tagline { get; set; }

    }

}
