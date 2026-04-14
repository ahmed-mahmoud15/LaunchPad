using Domain.Enums;

namespace Domain.Entities
{
    public class AssessmentQuestion
    {
        public int Id { get; set; }
        public int AssessmentId { get; set; }
        public int QuestionId { get; set; }
        public SubmissionStatus Status { get; set; } = SubmissionStatus.NotAttempted;
        public string? CodeSubmitted { get; set; }
        public string LanguageUsed { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

        public int? RunTime { get; set; }
        public long? Memory { get; set; }
        public int? TestCasesPasses { get; set; }
        public int? TotalTestCases {  get; set; }

        public Assessment Assessment { get; set; }
        public CodingQuestion Question { get; set; }
    }
}