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
        private readonly ICvAnalysisService analysisService;

        public CvController(IUserCvService cvService, ICvAnalysisService analysisService)
        {
            this.cvService = cvService;
            this.analysisService = analysisService;
        }

        [HttpGet("{userId:int}")]
        public async Task<IActionResult> GetAllForUser(int userId)
        {
            if (userId != CurrentUserId)
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
                return Unauthorized("You are not allowed to access this content");

            var result = await cvService.GetCvFileAsync(userId, cvId);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result.ErrorMessage);

            // inline = browser opens PDF in tab
            Response.Headers.Append(
                "Content-Disposition",
                $"inline; filename=\"{result.Value.FileName}\""
            );

            return File(result.Value.FileStream, result.Value.ContentType);
        }

        // Forces browser to download the file
        [HttpGet("{userId:int}/download/{cvId:int}")]
        public async Task<IActionResult> DownloadCv(int userId, int cvId)
        {
            if (userId != CurrentUserId)
                return Unauthorized("You are not allowed to access this content");

            var result = await cvService.GetCvFileAsync(userId, cvId);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result.ErrorMessage);

            // attachment = forces download with filename
            return File(
                result.Value.FileStream,
                result.Value.ContentType,
                result.Value.FileName      // this sets Content-Disposition: attachment
            );
        }



        [HttpPost("{userId:int}/upload")]
        public async Task<IActionResult> Upload(int userId, [FromForm] UploadCvDto dto)
        {
            if (userId != CurrentUserId)
            {
                return Unauthorized("You are not allowed to access this content");
            }

            var result = await cvService.UploadCvAsync(userId, dto);
            return StatusCode(result.StatusCode, result.IsSuccess ? result.Value : result.ErrorMessage);
        }

        [HttpPost("{userId:int}/analyze")]
        public async Task<IActionResult> Analyze(int userId, [FromForm] AnalyzeCvRequestDto request)
        {
            if (userId != CurrentUserId)
            {
                return Unauthorized("You are not allowed to access this content");
            }
            var result = await analysisService.AnalyzeCvAsync(userId, request);
            return StatusCode(result.StatusCode, result.IsSuccess ? result.Value : result.ErrorMessage);
        }
    }
}
