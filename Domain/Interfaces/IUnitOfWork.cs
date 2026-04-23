using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ICvRepository UserCvs { get; }
        IRepository<UserExperience> UserExperiences { get; }
        IRepository<UserEducation> UserEducations { get; }
        ISkillRepository Skills { get; }
        IRepository<Job> Jobs { get; }
        IJobTrackRepository JobTracks { get; }
        IApplicationRepository ApplicationHistory { get; }
        IAssessmentsRepository Assessments { get; }
        IRepository<AssessmentQuestion> AssessmentQuestions { get; }
        IRepository<CodingQuestion> CodingQuestions { get; }
        IRepository<QuestionTopic> QuestionTopics { get; }
        IRepository<HrQuestion> HrQuestions { get; }
        IInterviewRepository Interviews { get; }
        IRepository<InterviewQuestion> InterviewQuestions { get; }
        IRepository<CvJobAnalysis> CvJobAnalyses { get; }
        IRepository<CodingQuestionTopic> CodingQuestionTopics { get; }

        Task<int> SaveChangesAsync();
    }
}