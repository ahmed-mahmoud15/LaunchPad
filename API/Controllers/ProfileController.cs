using Application.DTOs.User;
using Application.Interfaces;
using Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : BaseController
    {
        private readonly IUserProfileService profileService;

        public ProfileController(IUserProfileService profileService)
        {
            this.profileService = profileService;
        }

        // GET api/profile/{id}/activity
        [HttpGet("{id:int}/activity")]
        public async Task<IActionResult> GetRecentActivity(int id)
        {
            if (CurrentUserId != id && Role != "Admin") {
                return Unauthorized("You Can't Access this content");
            }
            var result = await profileService.GetRecentActivitesAsync(id);
            return StatusCode(result.StatusCode, result.IsSuccess ? result.Value : result.ErrorMessage);
        }


        // GET api/profile/{id}/job-tracks?pageNumber=1&pageSize=10
        [HttpGet("{id:int}/job-tracks")]
        public async Task<IActionResult> GetJobTracks(int id, [FromQuery] PagedRequest request)
        {
            if (CurrentUserId != id && Role != "Admin")
            {
                return Unauthorized("You Can't Access this content");
            }
            var result = await profileService.GetJobTracksAsync(id, request);
            return StatusCode(result.StatusCode, result.IsSuccess ? result.Value : result.ErrorMessage);
        }

        // GET api/profile/{id}/assessments?pageNumber=1&pageSize=10
        [HttpGet("{id:int}/assessments")]
        public async Task<IActionResult> GetAssessments(int id, [FromQuery] ProfileQueryRequest request)
        {
            if (CurrentUserId != id && Role != "Admin")
            {
                return Unauthorized("You Can't Access this content");
            }
            var result = await profileService.GetAssessmentsAsync(id, request);
            return StatusCode(result.StatusCode, result.IsSuccess ? result.Value : result.ErrorMessage);
        }

        // GET api/profile/{id}/interviews?pageNumber=1&pageSize=10
        [HttpGet("{id:int}/interviews")]
        public async Task<IActionResult> GetInterviews(int id, [FromQuery] ProfileQueryRequest request)
        {
            if (CurrentUserId != id && Role != "Admin")
            {
                return Unauthorized("You Can't Access this content");
            }
            var result = await profileService.GetInterviewsAsync(id, request);
            return StatusCode(result.StatusCode, result.IsSuccess ? result.Value : result.ErrorMessage);
        }

        // GET api/profile/{id}/cv-analyses?pageNumber=1&pageSize=10
        [HttpGet("{id:int}/cv-analyses")]
        public async Task<IActionResult> GetCvAnalyses(int id, [FromQuery] ProfileQueryRequest request)
        {
            if (CurrentUserId != id && Role != "Admin")
            {
                return Unauthorized("You Can't Access this content");
            }
            var result = await profileService.GetCvAnalysesAsync(id, request);
            return StatusCode(result.StatusCode, result.IsSuccess ? result.Value : result.ErrorMessage);
        }
    }
}
