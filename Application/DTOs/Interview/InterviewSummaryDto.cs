using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Interview
{
    public class InterviewSummaryDto
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }

    }
}
