using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAssessmentsRepository : IRepository<Assessment>
    {
        Task<PagedResponse<Assessment>> GetAssessmentForUserPaginatedAsync(int userId, PagedRequest request);
    }
}
