using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Assessment_Engine;
using Domain.Common;

namespace Application.Interfaces
{
    public interface IAssessmentService
    {
        Task<Result<IEnumerable<ProblemDto>>> GetProblemsAsync(int userId, int easyCount, int mediumCount, int hardCount);
        Task<Result<SubmitResponseDto>> SubmitProblemAsync(SubmitRequestDto request, int userId, int assessmentQuestionId);
    }
}
