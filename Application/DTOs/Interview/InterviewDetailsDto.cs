using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Interview
{
    public class InterviewDetailsDto
    {
        public int Id { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
        public int Score { get; set; }
        public List<InterviewQuestionDetailsDto> Questions { get; set; } = new();
    }

    public class InterviewQuestionDetailsDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string QuestionAudio { get; set; }
        public string UserResponse { get; set; }
        public string? Feedback { get; set; }
        public double? Score { get; set; }
    }
}
