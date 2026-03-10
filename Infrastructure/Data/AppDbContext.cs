using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<UserExperience> UserExperiences { get; set; }
        DbSet<UserEducation>  UserEducations { get; set; }
        DbSet<UserCv> UserCvs { get; set; }
        DbSet<Skill> Skills { get; set; }
        DbSet<UserSkill> UserSkills { get; set; }
        DbSet<Job> Jobs { get; set; }
        DbSet<JobTrack> JobTracks { get; set; }
        DbSet<JobSkill> JobSkills { get; set; }
        DbSet<ApplicationHistory> ApplicationHistory { get; set; }
        DbSet<Assessment> Assessments { get; set; }
        DbSet<AssessmentQuestion> AssessmentQuestions { get; set; } 
        DbSet<CodingQuestion> CodingQuestions { get; set; }
        DbSet<HrQuestion> HrQuestions { get; set; }
        DbSet<Interview>  Interviews { get; set; }
        DbSet<InterviewQuestion> InterviewQuestions { get; set; }
        DbSet<QuestionTopic> QuestionTopics { get; set; }
        DbSet<CvJobAnalysis> CvJobs { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
