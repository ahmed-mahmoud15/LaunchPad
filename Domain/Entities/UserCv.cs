namespace Domain.Entities
{
    public class UserCv
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public bool IsDefault { get; set; } = false;
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public int Score { get; set; }

        public User User { get; set; }
        public ICollection<Job> Jobs { get; set; } = new List<Job>();
        public ICollection<CvJobAnalysis> CvJobAnalyses { get; set; } = new List<CvJobAnalysis>();
    }
}