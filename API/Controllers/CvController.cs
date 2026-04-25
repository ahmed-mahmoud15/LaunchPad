using Application.DTOs.Cv;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CvController : BaseController
    {
        private readonly IUserCvService cvService;

        public CvController(IUserCvService cvService) {
            this.cvService = cvService;
        }

        [HttpGet("{userId:int}")]
        public async Task<IActionResult> GetAllForUser(int userId)
        {
            if(userId != CurrentUserId)
            {
                return Unauthorized("You are not allowed to access this content");
            }

            var result = await cvService.GetUserCvsAsync(userId);
            return StatusCode(result.StatusCode, result.IsSuccess ? result.Value : result.ErrorMessage);
        }

        [HttpGet("{userId:int}/preview/{cvId:int}")]
        public async Task<IActionResult> PreviewCv(int userId, int cvId)
        {
            if (userId != CurrentUserId)
            {
                return Unauthorized("You are not allowed to access this content");
            }

            var result = await cvService.GetCvByIdAsync(userId, cvId);
            return StatusCode(result.StatusCode, result.IsSuccess ? result.Value : result.ErrorMessage);
        }

        

        [HttpPost("{userId:int}/upload")]
        public async Task<IActionResult> Upload(int userId,[FromForm] UploadCvDto dto)
        {
            if (userId != CurrentUserId)
            {
                return Unauthorized("You are not allowed to access this content");
            }

            var result = await cvService.UploadCvAsync(userId, dto);
            return StatusCode(result.StatusCode, result.IsSuccess ? result.Value : result.ErrorMessage);
        }
    }
}
