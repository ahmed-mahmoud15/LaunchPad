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
        private readonly IJobTrackingService jobService;

        public JobsController(IJobTrackingService jobService)
        {
            this.jobService = jobService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateJob([FromForm]CreateJobTrackDto dto)
        {
            var result = await jobService.CreateTrackedJob(CurrentUserId, dto);
            return StatusCode(result.StatusCode, !result.IsSuccess ? result.ErrorMessage : null);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateJobStatus(ChangeJobTrackStatusDto dto)
        {
            var result = await jobService.ChangeJobStatus(CurrentUserId, dto);
            return StatusCode(result.StatusCode, !result.IsSuccess ? result.ErrorMessage : null);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteJobTrack([FromQuery] int jobTrackId)
        {
            var result = await jobService.DeleteTrackedJob(CurrentUserId, jobTrackId);
            return StatusCode(result.StatusCode, !result.IsSuccess ? result.ErrorMessage : null);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> DisplayJobWithHistory(int id)
        {
            var result = await jobService.DisplayJobHistory(CurrentUserId, id);
            return StatusCode(result.StatusCode, !result.IsSuccess ? result.ErrorMessage : result.Value);
        }
    }
}
