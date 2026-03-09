using Domain.Enums;
using static System.Net.Mime.MediaTypeNames;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHashed { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateOnly JoinDate { get; set; }
        public bool IsActive { get; set; }
        public UserRole Role { get; set; }

        public ICollection<UserExperience> Experiences { get; set; } = new List<UserExperience>();
        public ICollection<UserEducation> Educations { get; set; } = new List<UserEducation>();
        public ICollection<UserSkill> UserSkills { get; set; } = new List<UserSkill>();
        public ICollection<UserCv> Cvs { get; set; } = new List<UserCv>();
        public ICollection<Assessment> Assessments { get; set; } = new List<Assessment>();
        public ICollection<Interview> Interviews { get; set; } = new List<Interview>();
        public ICollection<JobTrack> AppliedJobs { get; set; } = new List<JobTrack>();
        public ICollection<CvJobAnalysis> AnalysedJobs { get; set; } = new List<CvJobAnalysis>();
    }
}
