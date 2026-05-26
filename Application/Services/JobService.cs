using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Jobs;
using Application.Interfaces;
using Domain.Common;
using Domain.Interfaces;

namespace Application.Services
{
    public class JobService : IJobService
    {
        private readonly IUnitOfWork unit;
        public JobService(IUnitOfWork unit)
        {
            this.unit = unit;
        }

        public async Task<Result<IEnumerable<JobDto>>> GetAllUntrackedJobs(int userId)
        {
            var jobs = await unit.Jobs.FindAllAsync(j => j.UserId == userId && j.JobTrack == null);
            if(jobs is null)
            {
                return Result<IEnumerable<JobDto>>.NotFound("There is not user with this id");
            }

            

            var result = jobs.Select(j => new JobDto
            {
                JobId = j.Id,
                JotTitle = j.Title,
                JobType = j.Type.ToString()
            }).ToList();

            return Result<IEnumerable<JobDto>>.Ok(result);
        }

        public async Task<Result<JobDto>> GetUntrackedJob(int userId, int jobId)
        {
            var job = await unit.Jobs.FindAsync(j => j.UserId == userId && j.Id == jobId && j.JobTrack == null);

            if(job is null)
            {
                return Result<JobDto>.NotFound("There is no user or job with these ids");
            }

            var result = new JobDto
            {
                JobId = job.Id,
                JotTitle = job.Title,
                JobType = job.Type.ToString()
            };

            return Result<JobDto>.Ok(result);
        }
    }
}
