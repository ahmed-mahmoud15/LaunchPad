using Domain.Enums;

namespace Domain.Entities
{
    public class CodingQuestion
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ExampleInput { get; set; }
        public string? ExampleOutput { get; set; }
        public int TopicId { get; set; }
        public QuestionDifficulty Difficulty { get; set; }

        public QuestionTopic Topic { get; set; }
    }
}
