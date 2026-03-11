using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserExperience> UserExperiences { get; set; }
        public DbSet<UserEducation>  UserEducations { get; set; }
        public DbSet<UserCv> UserCvs { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobTrack> JobTracks { get; set; }
        public DbSet<JobSkill> JobSkills { get; set; }
        public DbSet<ApplicationHistory> ApplicationHistory { get; set; }
        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<AssessmentQuestion> AssessmentQuestions { get; set; } 
        public DbSet<CodingQuestion> CodingQuestions { get; set; }
        public DbSet<HrQuestion> HrQuestions { get; set; }
        public DbSet<Interview>  Interviews { get; set; }
        public DbSet<InterviewQuestion> InterviewQuestions { get; set; }
        public DbSet<QuestionTopic> QuestionTopics { get; set; }
        public DbSet<CvJobAnalysis> CvJobs { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
