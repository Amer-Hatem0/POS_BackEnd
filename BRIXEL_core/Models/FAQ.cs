using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIXEL_core.Models
{
    public class FAQ
    {
        public int Id { get; set; }

        public string QuestionAr { get; set; }
        public string AnswerAr { get; set; }

        public string QuestionEn { get; set; }
        public string AnswerEn { get; set; }

        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; } = true;
    }

}
