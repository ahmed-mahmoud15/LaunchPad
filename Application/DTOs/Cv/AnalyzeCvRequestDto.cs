using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Validations;
using Microsoft.AspNetCore.Http;

namespace Application.DTOs.Cv
{
    [ValidCv]
    [ValidJob]
    public class AnalyzeCvRequestDto : ICvRequest
    {
        public int? CvId { get; set; }
        public IFormFile? File { get; set; }
        public int? JobId { get; set; }
        public string? JobDescription { get; set; }
        public string? JobTitle { get; set; }
    }
}
