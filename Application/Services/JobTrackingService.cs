using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Job_Tracker;
using Application.Interfaces;
using Domain.Common;

namespace Application.Services
{
    public class JobTrackingService : IJobTrackingService
    {
        public Task<Result> ChangeJobStatus(int userId, ChangeJobTrackStatusDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<Result> CreateTrackedJob(int userId, CreateJobTrackDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<Result> DeleteTrackedJob(int userId, int jobTrackId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<ViewJobTrackDetailsDto>> DisplayJobHistory(int userId, int jobTrackId)
        {
            throw new NotImplementedException();
        }
    }
}
