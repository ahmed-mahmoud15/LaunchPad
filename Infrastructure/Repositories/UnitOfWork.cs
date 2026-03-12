using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private IRepository<User>? _users;
        private IRepository<UserCv>? _userCvs;
        private IRepository<UserExperience>? _userExperiences;
        private IRepository<UserEducation>? _userEducations;
        private IRepository<UserSkill>? _userSkills;
        private IRepository<Skill>? _skills;
        private IRepository<Job>? _jobs;
        private IRepository<JobTrack>? _jobTracks;
        private IRepository<JobSkill>? _jobSkills;
        private IRepository<ApplicationHistory>? _applicationHistory;
        private IRepository<Assessment>? _assessments;
        private IRepository<AssessmentQuestion>? _assessmentQuestions;
        private IRepository<CodingQuestion>? _codingQuestions;
        private IRepository<QuestionTopic>? _questionTopics;
        private IRepository<HrQuestion>? _hrQuestions;
        private IRepository<Interview>? _interviews;
        private IRepository<InterviewQuestion>? _interviewQuestions;
        private IRepository<CvJobAnalysis>? _cvJobAnalyses;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IRepository<User> Users =>
            _users ??= new Repository<User>(_context);

        public IRepository<UserCv> UserCvs =>
            _userCvs ??= new Repository<UserCv>(_context);

        public IRepository<UserExperience> UserExperiences =>
            _userExperiences ??= new Repository<UserExperience>(_context);

        public IRepository<UserEducation> UserEducations =>
            _userEducations ??= new Repository<UserEducation>(_context);

        public IRepository<UserSkill> UserSkills =>
            _userSkills ??= new Repository<UserSkill>(_context);

        public IRepository<Skill> Skills =>
            _skills ??= new Repository<Skill>(_context);

        public IRepository<Job> Jobs =>
            _jobs ??= new Repository<Job>(_context);

        public IRepository<JobTrack> JobTracks =>
            _jobTracks ??= new Repository<JobTrack>(_context);

        public IRepository<JobSkill> JobSkills =>
            _jobSkills ??= new Repository<JobSkill>(_context);

        public IRepository<ApplicationHistory> ApplicationHistory =>
            _applicationHistory ??= new Repository<ApplicationHistory>(_context);

        public IRepository<Assessment> Assessments =>
            _assessments ??= new Repository<Assessment>(_context);

        public IRepository<AssessmentQuestion> AssessmentQuestions =>
            _assessmentQuestions ??= new Repository<AssessmentQuestion>(_context);

        public IRepository<CodingQuestion> CodingQuestions =>
            _codingQuestions ??= new Repository<CodingQuestion>(_context);

        public IRepository<QuestionTopic> QuestionTopics =>
            _questionTopics ??= new Repository<QuestionTopic>(_context);

        public IRepository<HrQuestion> HrQuestions =>
            _hrQuestions ??= new Repository<HrQuestion>(_context);

        public IRepository<Interview> Interviews =>
            _interviews ??= new Repository<Interview>(_context);

        public IRepository<InterviewQuestion> InterviewQuestions =>
            _interviewQuestions ??= new Repository<InterviewQuestion>(_context);

        public IRepository<CvJobAnalysis> CvJobAnalyses =>
            _cvJobAnalyses ??= new Repository<CvJobAnalysis>(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}