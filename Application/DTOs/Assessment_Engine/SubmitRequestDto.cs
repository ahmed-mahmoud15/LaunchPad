using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Assessment_Engine
{
    public class SubmitRequestDto
    {
        public string lang { get; set; }
        public int questionId { get; set; }
        public string typedCode { get; set; }
    }
}
