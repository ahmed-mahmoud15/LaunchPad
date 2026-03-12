using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // ── Skills ────────────────────────────────────────────────────────────
            modelBuilder.Entity<Skill>().HasData(
                new Skill { Id = 1, Name = "C#" },
                new Skill { Id = 2, Name = "ASP.NET Core" },
                new Skill { Id = 3, Name = "SQL Server" },
                new Skill { Id = 4, Name = "React" },
                new Skill { Id = 5, Name = "Python" },
                new Skill { Id = 6, Name = "Docker" },
                new Skill { Id = 7, Name = "Git" },
                new Skill { Id = 8, Name = "JavaScript" },
                new Skill { Id = 9, Name = "TypeScript" },
                new Skill { Id = 10, Name = "Azure" }
            );

            // ── QuestionTopics ────────────────────────────────────────────────────
            modelBuilder.Entity<QuestionTopic>().HasData(
                new QuestionTopic { Id = 1, Name = "Arrays & Strings" },
                new QuestionTopic { Id = 2, Name = "Linked Lists" },
                new QuestionTopic { Id = 3, Name = "Trees & Graphs" },
                new QuestionTopic { Id = 4, Name = "Dynamic Programming" },
                new QuestionTopic { Id = 5, Name = "Sorting & Searching" }
            );

            // ── HrQuestions ───────────────────────────────────────────────────────
            modelBuilder.Entity<HrQuestion>().HasData(
                new HrQuestion
                {
                    Id = 1,
                    Question = "Tell me about yourself.",
                    ModelAnswer = "Give a concise professional summary: current role, key skills, relevant experience, and why you are excited about this opportunity."
                },
                new HrQuestion
                {
                    Id = 2,
                    Question = "What is your greatest strength?",
                    ModelAnswer = "Pick one strength directly relevant to the role, back it up with a concrete example, and tie it to the value you will bring."
                },
                new HrQuestion
                {
                    Id = 3,
                    Question = "Describe a challenging situation and how you resolved it.",
                    ModelAnswer = "Use the STAR method: describe the Situation, your Task, the Actions you took, and the positive Result."
                },
                new HrQuestion
                {
                    Id = 4,
                    Question = "Where do you see yourself in five years?",
                    ModelAnswer = "Show ambition that aligns with the company's growth: mention skills you want to develop and how the role fits your long-term career path."
                },
                new HrQuestion
                {
                    Id = 5,
                    Question = "Why do you want to work here?",
                    ModelAnswer = "Reference specific products, culture, or mission that genuinely excites you."
                }
            );

            // ── CodingQuestions ───────────────────────────────────────────────────
            modelBuilder.Entity<CodingQuestion>().HasData(
                new CodingQuestion
                {
                    Id = 1,
                    TopicId = 1,
                    Difficulty = QuestionDifficulty.Easy,
                    Title = "Two Sum",
                    Description = "Given an array of integers and a target, return the indices of two numbers that add up to the target.",
                    ExampleInput = "nums = [2,7,11,15], target = 9",
                    ExampleOutput = "[0,1]"
                },
                new CodingQuestion
                {
                    Id = 2,
                    TopicId = 2,
                    Difficulty = QuestionDifficulty.Easy,
                    Title = "Reverse a Linked List",
                    Description = "Reverse a singly linked list iteratively.",
                    ExampleInput = "1 -> 2 -> 3 -> 4 -> 5",
                    ExampleOutput = "5 -> 4 -> 3 -> 2 -> 1"
                },
                new CodingQuestion
                {
                    Id = 3,
                    TopicId = 3,
                    Difficulty = QuestionDifficulty.Medium,
                    Title = "Binary Tree Level Order Traversal",
                    Description = "Return the level-order (BFS) traversal of a binary tree's node values as a list of lists.",
                    ExampleInput = "[3,9,20,null,null,15,7]",
                    ExampleOutput = "[[3],[9,20],[15,7]]"
                },
                new CodingQuestion
                {
                    Id = 4,
                    TopicId = 4,
                    Difficulty = QuestionDifficulty.Medium,
                    Title = "Longest Common Subsequence",
                    Description = "Given two strings, return the length of their longest common subsequence.",
                    ExampleInput = "text1 = \"abcde\", text2 = \"ace\"",
                    ExampleOutput = "3"
                },
                new CodingQuestion
                {
                    Id = 5,
                    TopicId = 5,
                    Difficulty = QuestionDifficulty.Hard,
                    Title = "Merge K Sorted Arrays",
                    Description = "Merge k sorted arrays into one sorted array efficiently.",
                    ExampleInput = "[[1,4,7],[2,5,8],[3,6,9]]",
                    ExampleOutput = "[1,2,3,4,5,6,7,8,9]"
                }
            );

            // ── Users ─────────────────────────────────────────────────────────────
            // PasswordHashed values below are BCrypt hashes of "Password123!"
            // Replace with real hashes generated by BCrypt.Net.BCrypt.HashPassword("Password123!")
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Admin",
                    LastName = "LaunchPad",
                    Email = "admin@launchpad.io",
                    PasswordHashed = "$2a$11$REPLACE_WITH_REAL_BCRYPT_HASH_ADMIN",
                    PhoneNumber = "01000000000",
                    Address = "Cairo, Egypt",
                    JoinDate = new DateOnly(2024, 1, 1),
                    IsActive = true,
                    Role = UserRole.Admin
                },
                new User
                {
                    Id = 2,
                    FirstName = "Ahmed",
                    LastName = "Hassan",
                    Email = "ahmed.hassan@example.com",
                    PasswordHashed = "$2a$11$REPLACE_WITH_REAL_BCRYPT_HASH_AHMED",
                    PhoneNumber = "01012345678",
                    Address = "Giza, Egypt",
                    JoinDate = new DateOnly(2025, 3, 10),
                    IsActive = true,
                    Role = UserRole.User
                },
                new User
                {
                    Id = 3,
                    FirstName = "Sara",
                    LastName = "Ali",
                    Email = "sara.ali@example.com",
                    PasswordHashed = "$2a$11$REPLACE_WITH_REAL_BCRYPT_HASH_SARA",
                    PhoneNumber = "01123456789",
                    Address = "Alexandria, Egypt",
                    JoinDate = new DateOnly(2025, 6, 1),
                    IsActive = true,
                    Role = UserRole.User
                },
                new User
                {
                    Id = 4,
                    FirstName = "Omar",
                    LastName = "Khaled",
                    Email = "omar.khaled@example.com",
                    PasswordHashed = "$2a$11$REPLACE_WITH_REAL_BCRYPT_HASH_OMAR",
                    PhoneNumber = "01234567890",
                    Address = "Mansoura, Egypt",
                    JoinDate = new DateOnly(2025, 9, 15),
                    IsActive = true,
                    Role = UserRole.User
                }
            );

            // ── UserSkills ────────────────────────────────────────────────────────
            modelBuilder.Entity<UserSkill>().HasData(
                // Ahmed – C#, ASP.NET Core, SQL Server, Git
                new UserSkill { UserId = 2, SkillId = 1, Level = SkillLevel.Expert },
                new UserSkill { UserId = 2, SkillId = 2, Level = SkillLevel.Intermediate },
                new UserSkill { UserId = 2, SkillId = 3, Level = SkillLevel.Intermediate },
                new UserSkill { UserId = 2, SkillId = 7, Level = SkillLevel.Intermediate },
                // Sara – React, JavaScript, TypeScript, Git
                new UserSkill { UserId = 3, SkillId = 4, Level = SkillLevel.Expert },
                new UserSkill { UserId = 3, SkillId = 8, Level = SkillLevel.Expert },
                new UserSkill { UserId = 3, SkillId = 9, Level = SkillLevel.Intermediate },
                new UserSkill { UserId = 3, SkillId = 7, Level = SkillLevel.Beginner },
                // Omar – Python, Docker, Azure
                new UserSkill { UserId = 4, SkillId = 5, Level = SkillLevel.Intermediate },
                new UserSkill { UserId = 4, SkillId = 6, Level = SkillLevel.Beginner },
                new UserSkill { UserId = 4, SkillId = 10, Level = SkillLevel.Beginner }
            );

            // ── UserExperiences ───────────────────────────────────────────────────
            modelBuilder.Entity<UserExperience>().HasData(
                new UserExperience
                {
                    Id = 1,
                    UserId = 2,
                    Company = "Vodafone Egypt",
                    Title = "Backend Developer",
                    StartDate = new DateOnly(2022, 7, 1),
                    EndDate = new DateOnly(2024, 12, 31),
                    Description = "Built and maintained RESTful APIs using ASP.NET Core and SQL Server."
                },
                new UserExperience
                {
                    Id = 2,
                    UserId = 2,
                    Company = "Freelance",
                    Title = "Full-Stack Developer",
                    StartDate = new DateOnly(2021, 1, 1),
                    EndDate = new DateOnly(2022, 6, 30),
                    Description = "Delivered web applications for local SMEs using .NET and React."
                },
                new UserExperience
                {
                    Id = 3,
                    UserId = 3,
                    Company = "Swvl",
                    Title = "Frontend Developer",
                    StartDate = new DateOnly(2023, 3, 1),
                    EndDate = null,
                    Description = "Developed user-facing features with React and TypeScript in an agile team."
                },
                new UserExperience
                {
                    Id = 4,
                    UserId = 4,
                    Company = "Instabug",
                    Title = "Data Engineer Intern",
                    StartDate = new DateOnly(2024, 6, 1),
                    EndDate = new DateOnly(2024, 9, 30),
                    Description = "Built ETL pipelines with Python and managed cloud infrastructure on Azure."
                }
            );

            // ── UserEducations ────────────────────────────────────────────────────
            modelBuilder.Entity<UserEducation>().HasData(
                new UserEducation
                {
                    Id = 1,
                    UserId = 2,
                    University = "Cairo University",
                    Program = "Computer Science",
                    Degree = "Bachelor's",
                    StartDate = new DateOnly(2018, 9, 1),
                    EndDate = new DateOnly(2022, 6, 30),
                    Grade = "A"
                },
                new UserEducation
                {
                    Id = 2,
                    UserId = 3,
                    University = "Ain Shams University",
                    Program = "Software Engineering",
                    Degree = "Bachelor's",
                    StartDate = new DateOnly(2019, 9, 1),
                    EndDate = new DateOnly(2023, 6, 30),
                    Grade = "A+"
                },
                new UserEducation
                {
                    Id = 3,
                    UserId = 4,
                    University = "Mansoura University",
                    Program = "Information Technology",
                    Degree = "Bachelor's",
                    StartDate = new DateOnly(2020, 9, 1),
                    EndDate = new DateOnly(2024, 6, 30),
                    Grade = "B+"
                }
            );

            // ── UserCvs ───────────────────────────────────────────────────────────
            modelBuilder.Entity<UserCv>().HasData(
                new UserCv
                {
                    Id = 1,
                    UserId = 2,
                    FileName = "ahmed_hassan_cv.pdf",
                    FilePath = "cvs/2/ahmed_hassan_cv.pdf",
                    IsDefault = true,
                    UploadedAt = new DateTime(2025, 3, 11, 10, 0, 0, DateTimeKind.Utc),
                    Score = 82
                },
                new UserCv
                {
                    Id = 2,
                    UserId = 3,
                    FileName = "sara_ali_cv.pdf",
                    FilePath = "cvs/3/sara_ali_cv.pdf",
                    IsDefault = true,
                    UploadedAt = new DateTime(2025, 6, 2, 9, 0, 0, DateTimeKind.Utc),
                    Score = 91
                },
                new UserCv
                {
                    Id = 3,
                    UserId = 4,
                    FileName = "omar_khaled_cv.pdf",
                    FilePath = "cvs/4/omar_khaled_cv.pdf",
                    IsDefault = true,
                    UploadedAt = new DateTime(2025, 9, 16, 8, 0, 0, DateTimeKind.Utc),
                    Score = 74
                }
            );

            // ── Jobs ──────────────────────────────────────────────────────────────
            modelBuilder.Entity<Job>().HasData(
                new Job
                {
                    Id = 1,
                    UserId = 2,
                    CvId = 1,
                    Title = "Senior .NET Developer",
                    Info = "Build scalable microservices using .NET 9 and Azure.",
                    Type = JobType.FullTime
                },
                new Job
                {
                    Id = 2,
                    UserId = 3,
                    CvId = 2,
                    Title = "Frontend Engineer",
                    Info = "React / TypeScript SPA development for a fintech platform.",
                    Type = JobType.Remote
                },
                new Job
                {
                    Id = 3,
                    UserId = 4,
                    CvId = 3,
                    Title = "Data Engineer",
                    Info = "Python-based ETL pipelines on Azure Data Factory.",
                    Type = JobType.FullTime
                }
            );

            // ── JobTracks ─────────────────────────────────────────────────────────
            modelBuilder.Entity<JobTrack>().HasData(
                new JobTrack
                {
                    Id = 1,
                    JobId = 1,
                    CompanyName = "Microsoft Egypt",
                    Location = "Cairo, Egypt",
                    AppliedAt = new DateTime(2025, 10, 1, 12, 0, 0, DateTimeKind.Utc),
                    Salary = 25000.00m,
                    JobUrl = "https://careers.microsoft.com/job/1",
                    CurrentStatus = ApplicationStatus.Shortlisted
                },
                new JobTrack
                {
                    Id = 2,
                    JobId = 2,
                    CompanyName = "Noon",
                    Location = "Dubai, UAE",
                    AppliedAt = new DateTime(2025, 10, 5, 10, 0, 0, DateTimeKind.Utc),
                    Salary = 18000.00m,
                    JobUrl = "https://jobs.noon.com/job/2",
                    CurrentStatus = ApplicationStatus.Pending
                },
                new JobTrack
                {
                    Id = 3,
                    JobId = 3,
                    CompanyName = "IBM Egypt",
                    Location = "Cairo, Egypt",
                    AppliedAt = new DateTime(2025, 10, 10, 9, 0, 0, DateTimeKind.Utc),
                    Salary = 20000.00m,
                    JobUrl = "https://careers.ibm.com/job/3",
                    CurrentStatus = ApplicationStatus.Pending
                }
            );

            // ── JobSkills ─────────────────────────────────────────────────────────
            modelBuilder.Entity<JobSkill>().HasData(
                // Microsoft – C#, ASP.NET Core, SQL Server, Docker
                new JobSkill { JobTrackId = 1, SkillId = 1, RequiredLevel = SkillLevel.Expert },
                new JobSkill { JobTrackId = 1, SkillId = 2, RequiredLevel = SkillLevel.Expert },
                new JobSkill { JobTrackId = 1, SkillId = 3, RequiredLevel = SkillLevel.Intermediate },
                new JobSkill { JobTrackId = 1, SkillId = 6, RequiredLevel = SkillLevel.Beginner },
                // Noon – React, TypeScript
                new JobSkill { JobTrackId = 2, SkillId = 4, RequiredLevel = SkillLevel.Expert },
                new JobSkill { JobTrackId = 2, SkillId = 9, RequiredLevel = SkillLevel.Intermediate },
                // IBM – Python, Azure
                new JobSkill { JobTrackId = 3, SkillId = 5, RequiredLevel = SkillLevel.Intermediate },
                new JobSkill { JobTrackId = 3, SkillId = 10, RequiredLevel = SkillLevel.Intermediate }
            );

            // ── ApplicationHistory ────────────────────────────────────────────────
            modelBuilder.Entity<ApplicationHistory>().HasData(
                new ApplicationHistory
                {
                    Id = 1,
                    JobTrackId = 1,
                    Status = ApplicationStatus.Pending,
                    Notes = "Applied via company portal.",
                    UpdatedAt = new DateTime(2025, 10, 1, 12, 0, 0, DateTimeKind.Utc)
                },
                new ApplicationHistory
                {
                    Id = 2,
                    JobTrackId = 1,
                    Status = ApplicationStatus.Shortlisted,
                    Notes = "Recruiter reached out for a technical screen.",
                    UpdatedAt = new DateTime(2025, 10, 8, 9, 0, 0, DateTimeKind.Utc)
                },
                new ApplicationHistory
                {
                    Id = 3,
                    JobTrackId = 2,
                    Status = ApplicationStatus.Pending,
                    Notes = "Applied via LinkedIn Easy Apply.",
                    UpdatedAt = new DateTime(2025, 10, 5, 10, 0, 0, DateTimeKind.Utc)
                },
                new ApplicationHistory
                {
                    Id = 4,
                    JobTrackId = 3,
                    Status = ApplicationStatus.Pending,
                    Notes = "Applied through IBM careers page.",
                    UpdatedAt = new DateTime(2025, 10, 10, 9, 0, 0, DateTimeKind.Utc)
                }
            );

            // ── Assessments ───────────────────────────────────────────────────────
            modelBuilder.Entity<Assessment>().HasData(
                new Assessment
                {
                    Id = 1,
                    UserId = 2,
                    EasyCount = 2,
                    MediumCount = 2,
                    HardCount = 1,
                    CreatedAt = new DateTime(2025, 10, 15, 8, 0, 0, DateTimeKind.Utc),
                    StartedAt = new DateTime(2025, 10, 15, 9, 0, 0, DateTimeKind.Utc),
                    CompletedAt = new DateTime(2025, 10, 15, 10, 30, 0, DateTimeKind.Utc),
                    Score = 80
                },
                new Assessment
                {
                    Id = 2,
                    UserId = 3,
                    EasyCount = 1,
                    MediumCount = 2,
                    HardCount = 0,
                    CreatedAt = new DateTime(2025, 11, 1, 8, 0, 0, DateTimeKind.Utc),
                    StartedAt = new DateTime(2025, 11, 1, 9, 0, 0, DateTimeKind.Utc),
                    CompletedAt = new DateTime(2025, 11, 1, 9, 45, 0, DateTimeKind.Utc),
                    Score = 75
                },
                new Assessment
                {
                    Id = 3,
                    UserId = 4,
                    EasyCount = 2,
                    MediumCount = 1,
                    HardCount = 0,
                    CreatedAt = new DateTime(2025, 11, 10, 10, 0, 0, DateTimeKind.Utc),
                    StartedAt = null,
                    CompletedAt = null,
                    Score = 0
                }
            );

            // ── AssessmentQuestions ───────────────────────────────────────────────
            modelBuilder.Entity<AssessmentQuestion>().HasData(
                new AssessmentQuestion
                {
                    Id = 1,
                    AssessmentId = 1,
                    QuestionId = 1,
                    Status = SubmissionStatus.Accepted,
                    LanguageUsed = "C#",
                    CodeSubmitted = "public int[] TwoSum(int[] nums, int target) { var map = new Dictionary<int,int>(); for(int i=0;i<nums.Length;i++){int c=target-nums[i];if(map.ContainsKey(c))return new[]{map[c],i};map[nums[i]]=i;}return Array.Empty<int>(); }",
                    SubmittedAt = new DateTime(2025, 10, 15, 9, 20, 0, DateTimeKind.Utc)
                },
                new AssessmentQuestion
                {
                    Id = 2,
                    AssessmentId = 1,
                    QuestionId = 2,
                    Status = SubmissionStatus.Accepted,
                    LanguageUsed = "C#",
                    CodeSubmitted = "public ListNode ReverseList(ListNode head){ListNode prev=null,curr=head;while(curr!=null){var next=curr.next;curr.next=prev;prev=curr;curr=next;}return prev;}",
                    SubmittedAt = new DateTime(2025, 10, 15, 9, 50, 0, DateTimeKind.Utc)
                },
                new AssessmentQuestion
                {
                    Id = 3,
                    AssessmentId = 1,
                    QuestionId = 3,
                    Status = SubmissionStatus.WrongAnswer,
                    LanguageUsed = "C#",
                    CodeSubmitted = "// incomplete BFS attempt",
                    SubmittedAt = new DateTime(2025, 10, 15, 10, 10, 0, DateTimeKind.Utc)
                },
                new AssessmentQuestion
                {
                    Id = 4,
                    AssessmentId = 1,
                    QuestionId = 4,
                    Status = SubmissionStatus.Accepted,
                    LanguageUsed = "C#",
                    CodeSubmitted = "public int LCS(string a,string b){int[,] dp=new int[a.Length+1,b.Length+1];for(int i=1;i<=a.Length;i++)for(int j=1;j<=b.Length;j++)dp[i,j]=a[i-1]==b[j-1]?dp[i-1,j-1]+1:Math.Max(dp[i-1,j],dp[i,j-1]);return dp[a.Length,b.Length];}",
                    SubmittedAt = new DateTime(2025, 10, 15, 10, 25, 0, DateTimeKind.Utc)
                },
                new AssessmentQuestion
                {
                    Id = 5,
                    AssessmentId = 1,
                    QuestionId = 5,
                    Status = SubmissionStatus.TimeLimitExceeded,
                    LanguageUsed = "C#",
                    CodeSubmitted = "// naive merge - O(n^2)",
                    SubmittedAt = new DateTime(2025, 10, 15, 10, 28, 0, DateTimeKind.Utc)
                },
                new AssessmentQuestion
                {
                    Id = 6,
                    AssessmentId = 2,
                    QuestionId = 1,
                    Status = SubmissionStatus.Accepted,
                    LanguageUsed = "Python",
                    CodeSubmitted = "def twoSum(nums, target):\n    m={}\n    for i,n in enumerate(nums):\n        c=target-n\n        if c in m: return [m[c],i]\n        m[n]=i",
                    SubmittedAt = new DateTime(2025, 11, 1, 9, 15, 0, DateTimeKind.Utc)
                },
                new AssessmentQuestion
                {
                    Id = 7,
                    AssessmentId = 2,
                    QuestionId = 3,
                    Status = SubmissionStatus.Accepted,
                    LanguageUsed = "Python",
                    CodeSubmitted = "def levelOrder(root):\n    if not root: return []\n    q,res=[root],[]\n    while q:\n        res.append([n.val for n in q])\n        q=[c for n in q for c in[n.left,n.right] if c]\n    return res",
                    SubmittedAt = new DateTime(2025, 11, 1, 9, 35, 0, DateTimeKind.Utc)
                },
                new AssessmentQuestion
                {
                    Id = 8,
                    AssessmentId = 2,
                    QuestionId = 4,
                    Status = SubmissionStatus.WrongAnswer,
                    LanguageUsed = "Python",
                    CodeSubmitted = "# wrong dp transitions",
                    SubmittedAt = new DateTime(2025, 11, 1, 9, 43, 0, DateTimeKind.Utc)
                }
            );

            // ── Interviews ────────────────────────────────────────────────────────
            modelBuilder.Entity<Interview>().HasData(
                new Interview
                {
                    Id = 1,
                    UserId = 2,
                    StartedAt = new DateTime(2025, 10, 20, 14, 0, 0, DateTimeKind.Utc),
                    EndedAt = new DateTime(2025, 10, 20, 14, 30, 0, DateTimeKind.Utc),
                    Score = 85
                },
                new Interview
                {
                    Id = 2,
                    UserId = 3,
                    StartedAt = new DateTime(2025, 11, 5, 11, 0, 0, DateTimeKind.Utc),
                    EndedAt = new DateTime(2025, 11, 5, 11, 25, 0, DateTimeKind.Utc),
                    Score = 90
                },
                new Interview
                {
                    Id = 3,
                    UserId = 4,
                    StartedAt = new DateTime(2025, 11, 12, 10, 0, 0, DateTimeKind.Utc),
                    EndedAt = null,
                    Score = 0
                }
            );

            // ── InterviewQuestions ────────────────────────────────────────────────
            modelBuilder.Entity<InterviewQuestion>().HasData(
                new InterviewQuestion
                {
                    Id = 1,
                    InterviewId = 1,
                    QuestionId = 1,
                    UserResponse = "I am a backend developer with 3 years of experience in .NET. I enjoy solving complex system design problems and have recently been focusing on microservices.",
                    Feedback = "Good structure. Consider mentioning specific measurable achievements next time."
                },
                new InterviewQuestion
                {
                    Id = 2,
                    InterviewId = 1,
                    QuestionId = 2,
                    UserResponse = "My greatest strength is analytical thinking. For example, I reduced an API response time by 40% by profiling and refactoring the query layer.",
                    Feedback = "Excellent use of a concrete metric. Very compelling answer."
                },
                new InterviewQuestion
                {
                    Id = 3,
                    InterviewId = 1,
                    QuestionId = 3,
                    UserResponse = "I worked at a startup where a production bug caused downtime. I isolated the issue, deployed a hotfix, and then added regression tests to prevent recurrence.",
                    Feedback = "STAR method used well. Could elaborate more on the team collaboration aspect."
                },
                new InterviewQuestion
                {
                    Id = 4,
                    InterviewId = 2,
                    QuestionId = 1,
                    UserResponse = "I am a frontend engineer specialising in React. I love building accessible, performant UIs and collaborating closely with designers.",
                    Feedback = "Clear and confident. Good mention of soft skills."
                },
                new InterviewQuestion
                {
                    Id = 5,
                    InterviewId = 2,
                    QuestionId = 4,
                    UserResponse = "In five years I want to be a senior engineer leading the frontend architecture at a product company, mentoring junior developers.",
                    Feedback = "Ambitious and realistic. Well aligned with a growth-oriented company."
                },
                new InterviewQuestion
                {
                    Id = 6,
                    InterviewId = 3,
                    QuestionId = 1,
                    UserResponse = "I am a data engineering student interested in cloud pipelines and big data processing.",
                    Feedback = null
                }
            );

            // ── CvJobAnalyses ─────────────────────────────────────────────────────
            modelBuilder.Entity<CvJobAnalysis>().HasData(
                new CvJobAnalysis
                {
                    Id = 1,
                    UserId = 2,
                    CvId = 1,
                    JobId = 1,
                    Feedback = "Strong match. CV highlights C# and SQL Server experience well. Consider adding cloud certifications to strengthen the Azure requirement.",
                    Score = 88
                },
                new CvJobAnalysis
                {
                    Id = 2,
                    UserId = 3,
                    CvId = 2,
                    JobId = 2,
                    Feedback = "Excellent frontend skills. Portfolio projects with React and TypeScript are directly relevant. Minor gap: no fintech domain experience mentioned.",
                    Score = 92
                },
                new CvJobAnalysis
                {
                    Id = 3,
                    UserId = 4,
                    CvId = 3,
                    JobId = 3,
                    Feedback = "Good Python skills shown. Azure experience is limited to internship level; obtaining an AZ-900 certification would significantly improve this application.",
                    Score = 70
                }
            );
        }
    }
}
