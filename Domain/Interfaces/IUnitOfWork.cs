using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<UserCv> UserCvs { get; }
        IRepository<UserExperience> UserExperiences { get; }
        IRepository<UserEducation> UserEducations { get; }
        IRepository<UserSkill> UserSkills { get; }
        IRepository<Skill> Skills { get; }
        IRepository<Job> Jobs { get; }
        IRepository<JobTrack> JobTracks { get; }
        IRepository<JobSkill> JobSkills { get; }
        IRepository<ApplicationHistory> ApplicationHistory { get; }
        IRepository<Assessment> Assessments { get; }
        IRepository<AssessmentQuestion> AssessmentQuestions { get; }
        IRepository<CodingQuestion> CodingQuestions { get; }
        IRepository<QuestionTopic> QuestionTopics { get; }
        IRepository<HrQuestion> HrQuestions { get; }
        IRepository<Interview> Interviews { get; }
        IRepository<InterviewQuestion> InterviewQuestions { get; }
        IRepository<CvJobAnalysis> CvJobAnalyses { get; }

        Task<int> SaveChangesAsync();
    }
}