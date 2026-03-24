using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Assessment_Engine;

namespace Application.Interfaces
{
    public interface IAssessmentClient
    {
        Task<IEnumerable<ProblemDto>> ListAsync(int easyCount, int mediumCount, int hardCount);
        Task<SubmitResponseDto> SubmitAsync(string slug, SubmitRequestDto request);
    }
}
