namespace Domain.Entities
{
    public class CvJobAnalysis
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CvId { get; set; }
        public int JobId { get; set; }
        public string Feedback { get; set; }
        public int Score { get; set; }

        public User User { get; set; }
        public UserCv Cv { get; set; }
        public Job Job { get; set; }
    }
}