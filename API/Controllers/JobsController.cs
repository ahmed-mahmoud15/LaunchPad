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

        public JobsController(IJobTrackingService jobService) {
            this.jobService = jobService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateJob(CreateJobTrackDto dto)
        {
            var result = await jobService.CreateTrackedJob(CurrentUserId, dto);
            return StatusCode(result.StatusCode, !result.IsSuccess ? result.ErrorMessage : null);
        }
    }
}
