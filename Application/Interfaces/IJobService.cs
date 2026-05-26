using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Jobs;
using Domain.Common;

namespace Application.Interfaces
{
    public interface IJobService
    {
        Task<Result<IEnumerable<JobDto>>> GetAllUntrackedJobs(int userId);
        Task<Result<JobDto>> GetUntrackedJob(int userId, int jobId);
    }
}
