using Domain.Enums;

namespace Domain.Entities
{
    public class Job
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CvId { get; set; }
        public string Title { get; set; }
        public string? Info { get; set; }
        public JobType Type { get; set; }


        public User User { get; set; }
        public UserCv Cv { get; set; }
    }
}
