using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Domain.Enums;

namespace Application.DTOs.Assessment_Engine
{
    public class SolvedProblemDto : ProblemDto
    {
        public bool IsSolved { get; set; }
        public SubmissionStatus? Status { get; set; }
        public int? TotalTestCases { get; set; }
        public int? TestCasesPassed { get; set; }
        public int? RunTime { get; set; }
        public long? Memory {  get; set; }
    }
}
