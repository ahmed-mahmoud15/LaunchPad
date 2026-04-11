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
    }
}
