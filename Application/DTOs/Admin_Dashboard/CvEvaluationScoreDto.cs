using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Admin_Dashboard
{
    public class CvEvaluationScoreDto
    {
        public Dictionary<int, int> BelowAverage { get; set; } = new Dictionary<int, int>();
        public Dictionary<int ,int> AboveAverage { get; set; } = new Dictionary<int ,int>();
    }
}
