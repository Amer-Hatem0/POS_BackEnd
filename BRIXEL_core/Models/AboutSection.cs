using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace BRIXEL_core.Models
{
    public class AboutSection
    {
        [Key]
        public int Id { get; set; }

        public string TitleEn { get; set; } = "";
        public string TitleAr { get; set; } = "";

        public string DescriptionEn { get; set; } = "";
        public string DescriptionAr { get; set; } = "";

        public string ServicesEnJson { get; set; } = "[]";
        public string ServicesArJson { get; set; } = "[]";

        [NotMapped]
        public List<string> ServicesEn
        {
            get => JsonSerializer.Deserialize<List<string>>(ServicesEnJson) ?? new();
            set => ServicesEnJson = JsonSerializer.Serialize(value);
        }

        [NotMapped]
        public List<string> ServicesAr
        {
            get => JsonSerializer.Deserialize<List<string>>(ServicesArJson) ?? new();
            set => ServicesArJson = JsonSerializer.Serialize(value);
        }

        public int YearsOfExperience { get; set; }

        public string? MainImageUrl { get; set; }
        public string? SmallImageUrl { get; set; }
    }
}
