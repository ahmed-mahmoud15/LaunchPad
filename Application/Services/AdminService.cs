using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Admin_Dashboard;
using Application.Interfaces;
using Domain.Common;
using Domain.Interfaces;

namespace Application.Services
{
    public class AdminService : IAdminService
    {
        public readonly IUnitOfWork unit;
        public AdminService(IUnitOfWork unit) {
            this.unit = unit;
        }

        public async Task<Result<AssessmentPreferencesDto>> GetAssessmentPreferencesAsync()
        {
            var rawData = await unit.Assessments.GetAssessmentPreferencesRawAsync();

            return Result<AssessmentPreferencesDto>.Ok(new AssessmentPreferencesDto
            {
                EasyCount = rawData.EasyCount,
                MediumCount = rawData.MediumCount,
                HardCount = rawData.HardCount
            });
        }

        public async Task<Result<CardInfoDto>> GetAssessmentsCountAsync()
        {
            var rawData = await unit.Assessments.GetAssessmentCountAsync();
            return Result<CardInfoDto>.Ok(new CardInfoDto
            {
                CountPerMonth = rawData.CountPerMonth,
                TotalCount = rawData.TotalCount
            });
        }

        public async Task<Result<CardInfoDto>> GetCvAnalysesCountAsync()
        {
            var rawData = await unit.UserCvs.GetCvCountAsync();
            return Result<CardInfoDto>.Ok(new CardInfoDto
            {
                CountPerMonth = rawData.CountPerMonth,
                TotalCount = rawData.TotalCount
            });
        }

        public async Task<Result<CvEvaluationScoreDto>> GetCvEvaluationScoreAsync(int average)
        {
            var rawData = await unit.UserCvs.GetCvEvaluationAsync(average);
            return Result<CvEvaluationScoreDto>.Ok(new CvEvaluationScoreDto
            {
                BelowAverage = rawData.BelowAverage,
                AboveAverage = rawData.AboveAverage
            });
        }

        public async Task<Result<CardInfoDto>> GetInterviewsCountAsync()
        {
            var rawData = await unit.Interviews.GetInterviewCountAsync();
            return Result<CardInfoDto>.Ok(new CardInfoDto
            {
                CountPerMonth = rawData.CountPerMonth,
                TotalCount = rawData.TotalCount
            });
        }

        public async Task<Result<CardInfoDto>> GetUsersCountAsync()
        {
            var rawData = await unit.Users.GetUsersCountAsync();
            return Result<CardInfoDto>.Ok(new CardInfoDto
            {
                CountPerMonth = rawData.CountPerMonth,
                TotalCount = rawData.TotalCount
            });
        }
    }
}
