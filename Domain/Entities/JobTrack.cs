using Domain.Enums;

namespace Domain.Entities
{
    public class JobTrack
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string CompanyName { get; set; }
        public string? Location { get; set; }
        public DateTime AppliedAt { get; set; } = DateTime.UtcNow;
        public decimal? Salary { get; set; }
        public string? JobUrl { get; set; }
        public ApplicationStatus CurrentStatus { get; set; } = ApplicationStatus.Pending;

        public Job Job { get; set; }
        public ICollection<ApplicationHistory> History { get; set; } = new List<ApplicationHistory>();
        public ICollection<JobSkill> SkillsRequired { get; set; } = new List<JobSkill>();
    }
}