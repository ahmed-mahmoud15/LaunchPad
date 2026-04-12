using Application.Interfaces;
using Domain.RawData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        private readonly IAdminService adminService;
        public AdminController(IAdminService adminService) {
            this.adminService = adminService;
        }


        [HttpGet("assessment-preferences")]
        public async Task<IActionResult> GetAssessmentPreferences() {

            var result = await adminService.GetAssessmentPreferencesAsync();
            return Ok(result.Value);
        }

        [HttpGet("assessment-card")]
        public async Task<IActionResult> GetAssessmentCardCount()
        {
            var result = await adminService.GetAssessmentsCountAsync();
            return Ok(result.Value);
        }

        [HttpGet("user-card")]
        public async Task<IActionResult> GetUserCardCount()
        {
            var result = await adminService.GetUsersCountAsync();
            return Ok(result.Value);
        }

        [HttpGet("interview-card")]
        public async Task<IActionResult> GetInterviewCardCount()
        {
            var result = await adminService.GetInterviewsCountAsync();
            return Ok(result.Value);
        }

        [HttpGet("cv-card")]
        public async Task<IActionResult> GetCvCardCount()
        {
            var result = await adminService.GetCvAnalysesCountAsync();
            return Ok(result.Value);
        }

        [HttpGet("cv-evaluation/{average:int}")]
        public async Task<IActionResult> GetCvCardCount(int average)
        {
            var result = await adminService.GetCvEvaluationScoreAsync(average);
            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboard()
        {
            var userCard = await adminService.GetUsersCountAsync();
            var assessmentCard = await adminService.GetAssessmentsCountAsync();
            var interviewCard = await adminService.GetInterviewsCountAsync();
            var cvCard = await adminService.GetCvAnalysesCountAsync();
            var assessmentPreferences = await adminService.GetAssessmentPreferencesAsync();
            var cvEvalution = await adminService.GetCvEvaluationScoreAsync(75);
            return Ok(new
            {
                AssessmentCard = assessmentCard.Value,
                AssessmentPreferences = assessmentPreferences.Value,
                CvEvaluation = cvEvalution.Value,
                UserCard = userCard.Value,
                interviewCard = interviewCard.Value,
                cvCard = cvCard.Value
            });
        }

    }
}
