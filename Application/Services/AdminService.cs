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

        public Task<Result<CardInfoDto>> GetAssessmentsCountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<CardInfoDto>> GetCvAnalysesCountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<CvEvaluationScoreDto>> GetCvEvaluationScoreAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<CardInfoDto>> GetInterviewsCountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<CardInfoDto>> GetUsersCountAsync()
        {
            throw new NotImplementedException();
        }
    }
}
