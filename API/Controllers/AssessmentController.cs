using Application.DTOs.Assessment_Engine;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssessmentController : BaseController
    {
        private readonly IAssessmentService service;

        public AssessmentController(IAssessmentService service)
        {
            this.service = service;
        }

        [HttpGet("start")]
        public async Task<IActionResult> StartAssessment(
            [FromQuery] int easyCount,
            [FromQuery] int mediumCount,
            [FromQuery] int hardCount)
        {
            var result = await service.GetProblemsAsync(userId: CurrentUserId, easyCount: easyCount, mediumCount: mediumCount, hardCount: hardCount);
            return StatusCode(result.StatusCode, result.IsSuccess ? result.Value : result.ErrorMessage);
        }


        [HttpPost("submit/{assessmentQuestionId:int}")]
        public async Task<IActionResult> SubmitQuestion(int assessmentQuestionId, [FromBody] SubmitRequestDto dto)
        {
            var result = await service.SubmitProblemAsync(dto, CurrentUserId, assessmentQuestionId);
            return StatusCode(result.StatusCode, result.IsSuccess ? result.Value : result.ErrorMessage);
        }
    }
}
