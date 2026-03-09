namespace Domain.Entities
{
    public class InterviewQuestion
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string UserResponse { get; set; }
        public string? Feedback { get; set; }

        public Interview Interview { get; set; }
        public HrQuestion Question { get; set; }
    }
}
