using Application.Interfaces;
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
    }
}
