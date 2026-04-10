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
using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
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
            //var user = await unit.Users.GetByIdAsync(userId);

            //if(user is null)
            //{
            //    return Result<PagedResponse<AssessmentSummaryDto>>.NotFound("User Not Found");
            //}

            var assessments = await unit.Assessments.GetAssessmentForUserPaginatedAsync(userId, request);

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

        public async Task<Result<PagedResponse<CvAnalysisDto>>> GetCvAnalysesAsync(int userId, PagedRequest request)
        {
            //var user = await unit.Users.GetByIdAsync(userId);

            //if (user is null)
            //{
            //    return Result<PagedResponse<CvAnalysisDto>>.NotFound("User Not Found");
            //}

            var cvs = await unit.CvJobAnalyses.FindAllPaginatedAsync(request, a => a.UserId == userId);

            var result = new PagedResponse<CvAnalysisDto>()
            {
                PageNumber = cvs.PageNumber,
                PageSize = cvs.PageSize,
                TotalCount = cvs.TotalCount
            };

            foreach (var item in cvs.Items)
            {
                var dto = new CvAnalysisDto()
                {
                    Id = item.Id,
                    Score = item.Score,
                    Feedback = item.Feedback
                };
                result.Items.Add(dto);
            }

            return Result<PagedResponse<CvAnalysisDto>>.Ok(result);
        }

        public async Task<Result<PagedResponse<InterviewSummaryDto>>> GetInterviewsAsync(int userId, PagedRequest request)
        {
            //var user = await unit.Users.GetByIdAsync(userId);

            //if (user is null)
            //{
            //    return Result<PagedResponse<InterviewSummaryDto>>.NotFound("User Not Found");
            //}

            var interviews = await unit.Interviews.FindAllPaginatedAsync(request, a => a.UserId == userId);

            var result = new PagedResponse<InterviewSummaryDto>()
            {
                PageNumber = interviews.PageNumber,
                PageSize = interviews.PageSize,
                TotalCount = interviews.TotalCount
            };

            foreach (var item in interviews.Items)
            {
                var dto = new InterviewSummaryDto()
                {
                    Id = item.Id,
                    Score = item.Score,
                    StartedAt = item.StartedAt,
                    EndedAt = item.EndedAt ,
                };
                result.Items.Add(dto);
            }

            return Result<PagedResponse<InterviewSummaryDto>>.Ok(result);
        }

        public async Task<Result<PagedResponse<JobTrackDto>>> GetJobTracksAsync(int userId, PagedRequest request)
        {
            //var user = await unit.Users.GetByIdAsync(userId);

            //if (user is null)
            //{
            //    return Result<PagedResponse<JobTrackDto>>.NotFound("User Not Found");
            //}

            var jobs = await unit.JobTracks.GetJobTracksForUserPaginatedAsync(userId, request);

            var result = new PagedResponse<JobTrackDto>()
            {
                PageNumber = jobs.PageNumber,
                PageSize = jobs.PageSize,
                TotalCount = jobs.TotalCount
            };

            foreach (var item in jobs.Items)
            {
                var dto = new JobTrackDto()
                {
                    Id = item.Id,
                    CompanyName = item.CompanyName,
                    AppliedDate = item.AppliedAt,
                    JobDescription = item.Job.Info,
                    JobTitle = item.Job.Title,
                    JobType = item.Job.Type.ToString(),
                    Location = item.Location,
                    Status = item.CurrentStatus.ToString()
                };
                result.Items.Add(dto);
            }

            return Result<PagedResponse<JobTrackDto>>.Ok(result);
        }

        public async Task<Result<IEnumerable<UserActivityDto>>> GetRecentActivitesAsync(int userId)
        {
            var user = await unit.Users.GetUserWithAllEntitiesAsync(userId);

            return Result<IEnumerable<UserActivityDto>>.BadRequest(user.ToString());
        }
    }
}
