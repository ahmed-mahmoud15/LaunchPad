using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IJobTrackRepository : IRepository<JobTrack>
    {
        Task<PagedResponse<JobTrack>> GetJobTracksForUserPaginatedAsync(int userId, PagedRequest request);
    }
}
