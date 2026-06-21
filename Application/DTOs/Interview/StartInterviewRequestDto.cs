using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Interview
{
    public class StartInterviewRequestDto
    {
        public int? JobDescription {  get; set; }
        public int? JobId { get; set; }
        public int BehavioralCount { get; set; }
        public int TechnicalCount { get; set; }
        public int ResumeCount { get; set; }
    }
}
