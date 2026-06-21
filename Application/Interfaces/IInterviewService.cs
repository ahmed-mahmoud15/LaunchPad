using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Interview;
using Domain.Common;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface IInterviewService
    {
        Task<Result<StartInterviewResponseDto>> StartInterviewAsync(int userId, StartInterviewRequestDto dto);
        Task<Result<SubmitAnswerResponseDto>> SubmitAnswerAsync(int userId, int interviewQuestionId, IFormFile file);
        Task<Result<int>> EndInterviewAsync(int userId, int interviewId);
        Task<Result<InterviewDetailsDto>> GetInterviewDetailsAsync(int userId, int interviewId);
    }
}
