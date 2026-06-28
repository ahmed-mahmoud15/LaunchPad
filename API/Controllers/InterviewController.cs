using Application.DTOs.Interview;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InterviewController : BaseController
    {
        private readonly IInterviewService interviewService;

        public InterviewController(IInterviewService interviewService)
        {
            this.interviewService = interviewService;
        }

        [HttpPost("start")]
        public async Task<IActionResult> Start([FromForm] StartInterviewRequestDto dto)
        {
            var result = await interviewService.StartInterviewAsync(CurrentUserId, dto);
            return StatusCode(result.StatusCode, result.IsSuccess ? result.Value : result.ErrorMessage);
        }

        [HttpPost("answer/{interviewQuestionId:int}")]
        public async Task<IActionResult> SubmitAnswer(int interviewQuestionId, [FromForm]IFormFile? file, [FromForm]string? answer)
        {
            var result = await interviewService.SubmitAnswerAsync(CurrentUserId, interviewQuestionId, file, answer);
            return StatusCode(result.StatusCode, result.IsSuccess ? result.Value : result.ErrorMessage);
        }

        [HttpPut("{id:int}/end")]
        public async Task<IActionResult> End(int id)
        {
            var result = await interviewService.EndInterviewAsync(CurrentUserId, id);
            return StatusCode(result.StatusCode, result.IsSuccess ? result.Value : result.ErrorMessage);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var result = await interviewService.GetInterviewDetailsAsync(CurrentUserId, id);
            return StatusCode(result.StatusCode, result.IsSuccess ? result.Value : result.ErrorMessage);
        }
    }
}
