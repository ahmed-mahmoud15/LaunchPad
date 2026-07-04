using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Cv;
using Application.DTOs.CvAnalyzer;

namespace Application.Interfaces
{
    public interface ICvAnalyzerClient
    {
        public Task<CvAnalyzerResponseDto> EvaluateAsync(Stream pdfStream, string fileName, string jobDescription, string jobTitle);
    }
}
