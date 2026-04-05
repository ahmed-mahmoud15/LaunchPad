namespace Domain.Entities
{
    public class CodingQuestionTopic
    {
        public int CodingQuestionId { get; set; }
        public int QuestionTopicId { get; set; }

        public CodingQuestion CodingQuestion { get; set; }
        public QuestionTopic QuestionTopic { get; set; }
    }
}