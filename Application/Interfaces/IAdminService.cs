using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Admin_Dashboard;
using Domain.Common;

namespace Application.Interfaces
{
    public interface IAdminService
    {
        Task<Result<CardInfoDto>> GetUsersCountAsync();
        Task<Result<CardInfoDto>> GetAssessmentsCountAsync();
        Task<Result<CardInfoDto>> GetInterviewsCountAsync();
        Task<Result<CardInfoDto>> GetCvAnalysesCountAsync();
        Task<Result<AssessmentPreferencesDto>> GetAssessmentPreferencesAsync();
        Task<Result<CvEvaluationScoreDto>> GetCvEvaluationScoreAsync(int average);
        Task<Result> BanUser(int userId);
        Task<Result> ActivateUser(int userId);
    }
}
