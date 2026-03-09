using Domain.Enums;

namespace Domain.Entities
{
    public class AssessmentQuestion
    {
        public int Id { get; set; }
        public int AssessmentId { get; set; }
        public int QuestionId { get; set; }
        public SubmissionStatus Status { get; set; }
        public string CodeSubmitted { get; set; }
        public string LanguageUsed { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.Now;
        public Assessment Assessment { get; set; }
        public CodingQuestion Question { get; set; }
    }
}
