using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cv
{
    public class CvAnalysisDto
    {
        public int Id { get; set; }
        public string Feedback { get; set; }
        public int Score { get; set; }
        public string JobTitle { get; set; }
        public DateTime AnalyzeDate { get; set; }
    }
}
