using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Assessment_Engine;
using Application.DTOs.Cv;
using Application.DTOs.Interview;
using Application.DTOs.Job_Tracker;
using Application.DTOs.User;
using Application.Interfaces;
using Domain.Common;
using Domain.Interfaces;

namespace Application.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUnitOfWork unit;
        public UserProfileService(IUnitOfWork unit)
        {
            this.unit = unit;
        }

        public async Task<Result<PagedResponse<AssessmentSummaryDto>>> GetAssessmentsAsync(int userId, PagedRequest request)
        {
            var user = await unit.Users.GetByIdAsync(userId);

            if(user is null)
            {
                return Result<PagedResponse<AssessmentSummaryDto>>.NotFound("User Not Found");
            }

            var assessments = await unit.Assessments.FindAllPaginatedAsync(request, a => a.UserId == userId);

            var result = new PagedResponse<AssessmentSummaryDto>() {
                PageNumber = assessments.PageNumber,
                PageSize = assessments.PageSize,
                TotalCount = assessments.TotalCount
            };

            foreach(var item in assessments.Items)
            {
                var dto = new AssessmentSummaryDto() {
                    Id = item.Id,
                    CreatedAt = item.CreatedAt,
                    EasyCount = item.EasyCount,
                    HardCount = item.HardCount,
                    MediumCount = item.MediumCount,
                    Score = item.Score,
                    TotalCount = item.TotalCount
                };
                result.Items.Add(dto);
            }

            return Result<PagedResponse<AssessmentSummaryDto>>.Ok(result);
        }

        public Task<Result<PagedResponse<CvAnalysisDto>>> GetCvAnalysesAsync(int userId, PagedRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Result<PagedResponse<InterviewSummaryDto>>> GetInterviewsAsync(int userId, PagedRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Result<PagedResponse<JobTrackDto>>> GetJobTracksAsync(int userId, PagedRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<UserActivityDto>>> GetRecentActivitesAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
