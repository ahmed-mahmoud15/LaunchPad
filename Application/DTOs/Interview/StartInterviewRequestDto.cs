using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Validations;
using Microsoft.AspNetCore.Http;

namespace Application.DTOs.Interview
{
    [ValidCv]
    public class StartInterviewRequestDto : ICvRequest
    {
        public string? JobDescription {  get; set; }
        public int? JobId { get; set; }
        public int? CvId { get; set; }
        public IFormFile? File { get; set; }

        public int BehavioralCount { get; set; }
        public int TechnicalCount { get; set; }
        public int ResumeCount { get; set; }
    }
}
