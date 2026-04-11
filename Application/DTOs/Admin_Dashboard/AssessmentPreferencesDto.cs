using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Admin_Dashboard
{
    public class AssessmentPreferencesDto
    {
        public int EasyCount { get; set; }
        public int MediumCount { get; set; }
        public int HardCount { get; set; }
        public int TotalCount => EasyCount + MediumCount + HardCount;
        public double EasyPercentage => Math.Round((double)EasyCount / TotalCount * 100, 2);
        public double MediumPercentage => Math.Round((double)MediumCount / TotalCount * 100, 2);
        public double HardPercentage => Math.Round((double)HardCount / TotalCount * 100, 2);
    }
}
