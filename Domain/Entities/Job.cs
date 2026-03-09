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
        public string CompanyName { get; set; }
        public string? Location { get; set; }
        public DateTime AppliedAt { get; set; } = DateTime.Now;
        public JobType Type { get; set; }
        public decimal? Salary { get; set; }
        public string? JobUrl { get; set; }
        public ApplicationStatus CurrentStatus { get; set; } = ApplicationStatus.Pending;

        public User User { get; set; }
        public UserCv Cv { get; set; }
        public ICollection<ApplicationHistory> History { get; set; } = new List<ApplicationHistory>();
        public ICollection<JobSkill> SkillsRequired { get; set; } = new List<JobSkill>();

    }
}
