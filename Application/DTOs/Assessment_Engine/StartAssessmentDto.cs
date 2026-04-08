using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Assessment_Engine
{
    public class StartAssessmentDto
    {
        public IEnumerable<ProblemDto> Problems { get; set; }
        public int AssessmentId { get; set; }
    } 
}
