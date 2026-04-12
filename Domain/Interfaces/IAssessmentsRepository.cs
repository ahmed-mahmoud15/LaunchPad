using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Entities;
using Domain.RawData;

namespace Domain.Interfaces
{
    public interface IAssessmentsRepository : IRepository<Assessment>
    {
        Task<PagedResponse<Assessment>> GetAssessmentForUserPaginatedAsync(int userId, PagedRequest request);
        Task<AssessmentPreferencesRaw> GetAssessmentPreferencesRawAsync();
        Task<CardInfoRaw> GetAssessmentCountAsync();
        Task<Assessment> GetAssessmentWithIncludesAsync(int assessmentId);
    }
}
