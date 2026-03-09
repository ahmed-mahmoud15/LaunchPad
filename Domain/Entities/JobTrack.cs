using Domain.Enums;

namespace Domain.Entities
{
    public class JobTrack : Job
    {
        public string CompanyName { get; set; }
        public string? Location { get; set; }
        public DateTime AppliedAt { get; set; } = DateTime.Now;
        public decimal? Salary { get; set; }
        public string? JobUrl { get; set; }
        public ApplicationStatus CurrentStatus { get; set; } = ApplicationStatus.Pending;
        public ICollection<ApplicationHistory> History { get; set; } = new List<ApplicationHistory>();
        public ICollection<JobSkill> SkillsRequired { get; set; } = new List<JobSkill>();
    }
}
