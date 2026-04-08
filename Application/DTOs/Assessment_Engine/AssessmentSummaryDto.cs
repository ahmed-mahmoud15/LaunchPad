using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Assessment_Engine
{
    public class AssessmentSummaryDto
    {
        public int Id { get; set; }
        public int EasyCount { get; set; }
        public int MediumCount { get; set; }
        public int HardCount { get; set; }
        public int TotalCount {  get; set; }
        public int Score { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
