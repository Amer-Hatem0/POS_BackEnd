using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIXEL_core.DTOs
{
    public class FAQDto
    {
        public string QuestionAr { get; set; }
        public string AnswerAr { get; set; }

        public string QuestionEn { get; set; }
        public string AnswerEn { get; set; }

        public int DisplayOrder { get; set; }
    }
}
