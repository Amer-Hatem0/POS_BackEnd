using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIXEL_core.Models
{
    public class ProjectImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}