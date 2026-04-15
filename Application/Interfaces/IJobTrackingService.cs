using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Job_Tracker;
using Domain.Common;

namespace Application.Interfaces
{
    public interface IJobTrackingService
    {
        public Task<Result> CreateTrackedJob(int userId, CreateJobTrackDto dto);
        public Task<Result> DeleteTrackedJob(int userId, int jobTrackId);
        public Task<Result> ChangeJobStatus(int userId, ChangeJobTrackStatusDto dto);
        public Task<Result<ViewJobTrackDetailsDto>> DisplayJobHistory(int userId, int jobTrackId);
    }
}
