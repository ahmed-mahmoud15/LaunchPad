using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Interview
{
    public class StartInterviewResponseDto
    {
        public int InterviewId { get; set; }
        public List<InterviewQuestionItemDto> Questions { get; set; } = new();
        public string Role { get; set; }
    }

    public  class InterviewQuestionItemDto
    {
        public int InterviewQuestionId { get; set; }
        public string QuestionText { get; set; }
        public string AudioBase64 { get; set; }
    }
}
