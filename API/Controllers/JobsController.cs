using Application.DTOs.Job_Tracker;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JobsController : BaseController
    {
        private readonly IJobTrackingService jobTrackService;
        private readonly IJobService jobService;

        public JobsController(IJobTrackingService jobTrackService, IJobService jobService)
        {
            this.jobTrackService = jobTrackService;
            this.jobService = jobService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateJob([FromForm]CreateJobTrackDto dto)
        {
            var result = await jobTrackService.CreateTrackedJob(CurrentUserId, dto);
            return StatusCode(result.StatusCode, !result.IsSuccess ? result.ErrorMessage : null);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateJobStatus(ChangeJobTrackStatusDto dto)
        {
            var result = await jobTrackService.ChangeJobStatus(CurrentUserId, dto);
            return StatusCode(result.StatusCode, !result.IsSuccess ? result.ErrorMessage : null);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteJobTrack([FromQuery] int jobTrackId)
        {
            var result = await jobTrackService.DeleteTrackedJob(CurrentUserId, jobTrackId);
            return StatusCode(result.StatusCode, !result.IsSuccess ? result.ErrorMessage : null);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> DisplayJobWithHistory(int id)
        {
            var result = await jobTrackService.DisplayJobHistory(CurrentUserId, id);
            return StatusCode(result.StatusCode, !result.IsSuccess ? result.ErrorMessage : result.Value);
        }

        [HttpGet("untracked")]
        public async Task<IActionResult> GetAllUntrackedJobs()
        {
            var result = await jobService.GetAllUntrackedJobs(CurrentUserId);
            return StatusCode(result.StatusCode, !result.IsSuccess ? result.ErrorMessage : result.Value);
        }
        [HttpGet("untracked/{id:int}")]
        public async Task<IActionResult> GetUntrackedJobById(int id)
        {
            var result = await jobService.GetUntrackedJob(CurrentUserId, id);
            return StatusCode(result.StatusCode, !result.IsSuccess ? result.ErrorMessage : result.Value);
        }
    }
}
