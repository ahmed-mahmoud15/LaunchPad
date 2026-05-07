using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Assessment_Engine;
using Application.DTOs.Cv;
using Application.DTOs.Interview;
using Application.DTOs.Job_Tracker;
using Application.DTOs.User;
using Domain.Common;

namespace Application.Interfaces
{
    public interface IUserProfileService
    {
        Task<Result<IEnumerable<UserActivityDto>>> GetRecentActivitesAsync(int userId);
        Task<Result<PagedResponse<JobTrackDto>>> GetJobTracksAsync(int userId, PagedRequest request);
        Task<Result<PagedResponse<AssessmentSummaryDto>>> GetAssessmentsAsync(int userId, ProfileQueryRequest request);
        Task<Result<PagedResponse<InterviewSummaryDto>>> GetInterviewsAsync(int userId, ProfileQueryRequest request);
        Task<Result<PagedResponse<CvAnalysisDto>>> GetCvAnalysesAsync(int userId, ProfileQueryRequest request);
    }
}
