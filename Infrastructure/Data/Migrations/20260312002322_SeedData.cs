using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "HrQuestions",
                columns: new[] { "Id", "ModelAnswer", "Question" },
                values: new object[,]
                {
                    { 1, "Give a concise professional summary: current role, key skills, relevant experience, and why you are excited about this opportunity.", "Tell me about yourself." },
                    { 2, "Pick one strength directly relevant to the role, back it up with a concrete example, and tie it to the value you will bring.", "What is your greatest strength?" },
                    { 3, "Use the STAR method: describe the Situation, your Task, the Actions you took, and the positive Result.", "Describe a challenging situation and how you resolved it." },
                    { 4, "Show ambition that aligns with the company's growth: mention skills you want to develop and how the role fits your long-term career path.", "Where do you see yourself in five years?" },
                    { 5, "Reference specific products, culture, or mission that genuinely excites you.", "Why do you want to work here?" }
                });

            migrationBuilder.InsertData(
                table: "QuestionTopics",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Arrays & Strings" },
                    { 2, "Linked Lists" },
                    { 3, "Trees & Graphs" },
                    { 4, "Dynamic Programming" },
                    { 5, "Sorting & Searching" }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "C#" },
                    { 2, "ASP.NET Core" },
                    { 3, "SQL Server" },
                    { 4, "React" },
                    { 5, "Python" },
                    { 6, "Docker" },
                    { 7, "Git" },
                    { 8, "JavaScript" },
                    { 9, "TypeScript" },
                    { 10, "Azure" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Email", "FirstName", "IsActive", "JoinDate", "LastName", "PasswordHashed", "PhoneNumber", "Role" },
                values: new object[,]
                {
                    { 1, "Cairo, Egypt", "admin@launchpad.io", "Admin", true, new DateOnly(2024, 1, 1), "LaunchPad", "$2a$11$REPLACE_WITH_REAL_BCRYPT_HASH_ADMIN", "01000000000", 2 },
                    { 2, "Giza, Egypt", "ahmed.hassan@example.com", "Ahmed", true, new DateOnly(2025, 3, 10), "Hassan", "$2a$11$REPLACE_WITH_REAL_BCRYPT_HASH_AHMED", "01012345678", 1 },
                    { 3, "Alexandria, Egypt", "sara.ali@example.com", "Sara", true, new DateOnly(2025, 6, 1), "Ali", "$2a$11$REPLACE_WITH_REAL_BCRYPT_HASH_SARA", "01123456789", 1 },
                    { 4, "Mansoura, Egypt", "omar.khaled@example.com", "Omar", true, new DateOnly(2025, 9, 15), "Khaled", "$2a$11$REPLACE_WITH_REAL_BCRYPT_HASH_OMAR", "01234567890", 1 }
                });

            migrationBuilder.InsertData(
                table: "Assessments",
                columns: new[] { "Id", "CompletedAt", "CreatedAt", "EasyCount", "HardCount", "MediumCount", "Score", "StartedAt", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 15, 10, 30, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 15, 8, 0, 0, 0, DateTimeKind.Utc), 2, 1, 2, 80, new DateTime(2025, 10, 15, 9, 0, 0, 0, DateTimeKind.Utc), 2 },
                    { 2, new DateTime(2025, 11, 1, 9, 45, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 1, 8, 0, 0, 0, DateTimeKind.Utc), 1, 0, 2, 75, new DateTime(2025, 11, 1, 9, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 3, null, new DateTime(2025, 11, 10, 10, 0, 0, 0, DateTimeKind.Utc), 2, 0, 1, 0, null, 4 }
                });

            migrationBuilder.InsertData(
                table: "CodingQuestions",
                columns: new[] { "Id", "Description", "Difficulty", "ExampleInput", "ExampleOutput", "Title", "TopicId" },
                values: new object[,]
                {
                    { 1, "Given an array of integers and a target, return the indices of two numbers that add up to the target.", 1, "nums = [2,7,11,15], target = 9", "[0,1]", "Two Sum", 1 },
                    { 2, "Reverse a singly linked list iteratively.", 1, "1 -> 2 -> 3 -> 4 -> 5", "5 -> 4 -> 3 -> 2 -> 1", "Reverse a Linked List", 2 },
                    { 3, "Return the level-order (BFS) traversal of a binary tree's node values as a list of lists.", 2, "[3,9,20,null,null,15,7]", "[[3],[9,20],[15,7]]", "Binary Tree Level Order Traversal", 3 },
                    { 4, "Given two strings, return the length of their longest common subsequence.", 2, "text1 = \"abcde\", text2 = \"ace\"", "3", "Longest Common Subsequence", 4 },
                    { 5, "Merge k sorted arrays into one sorted array efficiently.", 3, "[[1,4,7],[2,5,8],[3,6,9]]", "[1,2,3,4,5,6,7,8,9]", "Merge K Sorted Arrays", 5 }
                });

            migrationBuilder.InsertData(
                table: "Interviews",
                columns: new[] { "Id", "EndedAt", "Score", "StartedAt", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 20, 14, 30, 0, 0, DateTimeKind.Utc), 85, new DateTime(2025, 10, 20, 14, 0, 0, 0, DateTimeKind.Utc), 2 },
                    { 2, new DateTime(2025, 11, 5, 11, 25, 0, 0, DateTimeKind.Utc), 90, new DateTime(2025, 11, 5, 11, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 3, null, 0, new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), 4 }
                });

            migrationBuilder.InsertData(
                table: "UserCvs",
                columns: new[] { "Id", "FileName", "FilePath", "IsDefault", "Score", "UploadedAt", "UserId" },
                values: new object[,]
                {
                    { 1, "ahmed_hassan_cv.pdf", "cvs/2/ahmed_hassan_cv.pdf", true, 82, new DateTime(2025, 3, 11, 10, 0, 0, 0, DateTimeKind.Utc), 2 },
                    { 2, "sara_ali_cv.pdf", "cvs/3/sara_ali_cv.pdf", true, 91, new DateTime(2025, 6, 2, 9, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 3, "omar_khaled_cv.pdf", "cvs/4/omar_khaled_cv.pdf", true, 74, new DateTime(2025, 9, 16, 8, 0, 0, 0, DateTimeKind.Utc), 4 }
                });

            migrationBuilder.InsertData(
                table: "UserEducations",
                columns: new[] { "Id", "Degree", "EndDate", "Grade", "Program", "StartDate", "University", "UserId" },
                values: new object[,]
                {
                    { 1, "Bachelor's", new DateOnly(2022, 6, 30), "A", "Computer Science", new DateOnly(2018, 9, 1), "Cairo University", 2 },
                    { 2, "Bachelor's", new DateOnly(2023, 6, 30), "A+", "Software Engineering", new DateOnly(2019, 9, 1), "Ain Shams University", 3 },
                    { 3, "Bachelor's", new DateOnly(2024, 6, 30), "B+", "Information Technology", new DateOnly(2020, 9, 1), "Mansoura University", 4 }
                });

            migrationBuilder.InsertData(
                table: "UserExperiences",
                columns: new[] { "Id", "Company", "Description", "EndDate", "StartDate", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, "Vodafone Egypt", "Built and maintained RESTful APIs using ASP.NET Core and SQL Server.", new DateOnly(2024, 12, 31), new DateOnly(2022, 7, 1), "Backend Developer", 2 },
                    { 2, "Freelance", "Delivered web applications for local SMEs using .NET and React.", new DateOnly(2022, 6, 30), new DateOnly(2021, 1, 1), "Full-Stack Developer", 2 },
                    { 3, "Swvl", "Developed user-facing features with React and TypeScript in an agile team.", null, new DateOnly(2023, 3, 1), "Frontend Developer", 3 },
                    { 4, "Instabug", "Built ETL pipelines with Python and managed cloud infrastructure on Azure.", new DateOnly(2024, 9, 30), new DateOnly(2024, 6, 1), "Data Engineer Intern", 4 }
                });

            migrationBuilder.InsertData(
                table: "UserSkills",
                columns: new[] { "SkillId", "UserId", "Level" },
                values: new object[,]
                {
                    { 1, 2, 3 },
                    { 2, 2, 2 },
                    { 3, 2, 2 },
                    { 7, 2, 2 },
                    { 4, 3, 3 },
                    { 7, 3, 1 },
                    { 8, 3, 3 },
                    { 9, 3, 2 },
                    { 5, 4, 2 },
                    { 6, 4, 1 },
                    { 10, 4, 1 }
                });

            migrationBuilder.InsertData(
                table: "AssessmentQuestions",
                columns: new[] { "Id", "AssessmentId", "CodeSubmitted", "LanguageUsed", "QuestionId", "Status", "SubmittedAt" },
                values: new object[,]
                {
                    { 1, 1, "public int[] TwoSum(int[] nums, int target) { var map = new Dictionary<int,int>(); for(int i=0;i<nums.Length;i++){int c=target-nums[i];if(map.ContainsKey(c))return new[]{map[c],i};map[nums[i]]=i;}return Array.Empty<int>(); }", "C#", 1, 2, new DateTime(2025, 10, 15, 9, 20, 0, 0, DateTimeKind.Utc) },
                    { 2, 1, "public ListNode ReverseList(ListNode head){ListNode prev=null,curr=head;while(curr!=null){var next=curr.next;curr.next=prev;prev=curr;curr=next;}return prev;}", "C#", 2, 2, new DateTime(2025, 10, 15, 9, 50, 0, 0, DateTimeKind.Utc) },
                    { 3, 1, "// incomplete BFS attempt", "C#", 3, 3, new DateTime(2025, 10, 15, 10, 10, 0, 0, DateTimeKind.Utc) },
                    { 4, 1, "public int LCS(string a,string b){int[,] dp=new int[a.Length+1,b.Length+1];for(int i=1;i<=a.Length;i++)for(int j=1;j<=b.Length;j++)dp[i,j]=a[i-1]==b[j-1]?dp[i-1,j-1]+1:Math.Max(dp[i-1,j],dp[i,j-1]);return dp[a.Length,b.Length];}", "C#", 4, 2, new DateTime(2025, 10, 15, 10, 25, 0, 0, DateTimeKind.Utc) },
                    { 5, 1, "// naive merge - O(n^2)", "C#", 5, 4, new DateTime(2025, 10, 15, 10, 28, 0, 0, DateTimeKind.Utc) },
                    { 6, 2, "def twoSum(nums, target):\n    m={}\n    for i,n in enumerate(nums):\n        c=target-n\n        if c in m: return [m[c],i]\n        m[n]=i", "Python", 1, 2, new DateTime(2025, 11, 1, 9, 15, 0, 0, DateTimeKind.Utc) },
                    { 7, 2, "def levelOrder(root):\n    if not root: return []\n    q,res=[root],[]\n    while q:\n        res.append([n.val for n in q])\n        q=[c for n in q for c in[n.left,n.right] if c]\n    return res", "Python", 3, 2, new DateTime(2025, 11, 1, 9, 35, 0, 0, DateTimeKind.Utc) },
                    { 8, 2, "# wrong dp transitions", "Python", 4, 3, new DateTime(2025, 11, 1, 9, 43, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "InterviewQuestions",
                columns: new[] { "Id", "Feedback", "InterviewId", "QuestionId", "UserResponse" },
                values: new object[,]
                {
                    { 1, "Good structure. Consider mentioning specific measurable achievements next time.", 1, 1, "I am a backend developer with 3 years of experience in .NET. I enjoy solving complex system design problems and have recently been focusing on microservices." },
                    { 2, "Excellent use of a concrete metric. Very compelling answer.", 1, 2, "My greatest strength is analytical thinking. For example, I reduced an API response time by 40% by profiling and refactoring the query layer." },
                    { 3, "STAR method used well. Could elaborate more on the team collaboration aspect.", 1, 3, "I worked at a startup where a production bug caused downtime. I isolated the issue, deployed a hotfix, and then added regression tests to prevent recurrence." },
                    { 4, "Clear and confident. Good mention of soft skills.", 2, 1, "I am a frontend engineer specialising in React. I love building accessible, performant UIs and collaborating closely with designers." },
                    { 5, "Ambitious and realistic. Well aligned with a growth-oriented company.", 2, 4, "In five years I want to be a senior engineer leading the frontend architecture at a product company, mentoring junior developers." },
                    { 6, null, 3, 1, "I am a data engineering student interested in cloud pipelines and big data processing." }
                });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "CvId", "Info", "Title", "Type", "UserId" },
                values: new object[,]
                {
                    { 1, 1, "Build scalable microservices using .NET 9 and Azure.", "Senior .NET Developer", 1, 2 },
                    { 2, 2, "React / TypeScript SPA development for a fintech platform.", "Frontend Engineer", 3, 3 },
                    { 3, 3, "Python-based ETL pipelines on Azure Data Factory.", "Data Engineer", 1, 4 }
                });

            migrationBuilder.InsertData(
                table: "CvJobAnalyses",
                columns: new[] { "Id", "CvId", "Feedback", "JobId", "Score", "UserId" },
                values: new object[,]
                {
                    { 1, 1, "Strong match. CV highlights C# and SQL Server experience well. Consider adding cloud certifications to strengthen the Azure requirement.", 1, 88, 2 },
                    { 2, 2, "Excellent frontend skills. Portfolio projects with React and TypeScript are directly relevant. Minor gap: no fintech domain experience mentioned.", 2, 92, 3 },
                    { 3, 3, "Good Python skills shown. Azure experience is limited to internship level; obtaining an AZ-900 certification would significantly improve this application.", 3, 70, 4 }
                });

            migrationBuilder.InsertData(
                table: "JobTracks",
                columns: new[] { "Id", "AppliedAt", "CompanyName", "CurrentStatus", "JobId", "JobUrl", "Location", "Salary" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Microsoft Egypt", 2, 1, "https://careers.microsoft.com/job/1", "Cairo, Egypt", 25000.00m },
                    { 2, new DateTime(2025, 10, 5, 10, 0, 0, 0, DateTimeKind.Utc), "Noon", 1, 2, "https://jobs.noon.com/job/2", "Dubai, UAE", 18000.00m },
                    { 3, new DateTime(2025, 10, 10, 9, 0, 0, 0, DateTimeKind.Utc), "IBM Egypt", 1, 3, "https://careers.ibm.com/job/3", "Cairo, Egypt", 20000.00m }
                });

            migrationBuilder.InsertData(
                table: "ApplicationHistory",
                columns: new[] { "Id", "JobTrackId", "Notes", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, "Applied via company portal.", 1, new DateTime(2025, 10, 1, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 1, "Recruiter reached out for a technical screen.", 2, new DateTime(2025, 10, 8, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 2, "Applied via LinkedIn Easy Apply.", 1, new DateTime(2025, 10, 5, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 3, "Applied through IBM careers page.", 1, new DateTime(2025, 10, 10, 9, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "JobSkills",
                columns: new[] { "JobTrackId", "SkillId", "RequiredLevel" },
                values: new object[,]
                {
                    { 1, 1, 3 },
                    { 1, 2, 3 },
                    { 1, 3, 2 },
                    { 1, 6, 1 },
                    { 2, 4, 3 },
                    { 2, 9, 2 },
                    { 3, 5, 2 },
                    { 3, 10, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationHistory",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ApplicationHistory",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ApplicationHistory",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ApplicationHistory",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AssessmentQuestions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AssessmentQuestions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AssessmentQuestions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AssessmentQuestions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AssessmentQuestions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AssessmentQuestions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AssessmentQuestions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AssessmentQuestions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Assessments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CvJobAnalyses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CvJobAnalyses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CvJobAnalyses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "HrQuestions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "InterviewQuestions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "InterviewQuestions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "InterviewQuestions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "InterviewQuestions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "InterviewQuestions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "InterviewQuestions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "JobSkills",
                keyColumns: new[] { "JobTrackId", "SkillId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "JobSkills",
                keyColumns: new[] { "JobTrackId", "SkillId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "JobSkills",
                keyColumns: new[] { "JobTrackId", "SkillId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "JobSkills",
                keyColumns: new[] { "JobTrackId", "SkillId" },
                keyValues: new object[] { 1, 6 });

            migrationBuilder.DeleteData(
                table: "JobSkills",
                keyColumns: new[] { "JobTrackId", "SkillId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "JobSkills",
                keyColumns: new[] { "JobTrackId", "SkillId" },
                keyValues: new object[] { 2, 9 });

            migrationBuilder.DeleteData(
                table: "JobSkills",
                keyColumns: new[] { "JobTrackId", "SkillId" },
                keyValues: new object[] { 3, 5 });

            migrationBuilder.DeleteData(
                table: "JobSkills",
                keyColumns: new[] { "JobTrackId", "SkillId" },
                keyValues: new object[] { 3, 10 });

            migrationBuilder.DeleteData(
                table: "UserEducations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserEducations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserEducations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UserExperiences",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserExperiences",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserExperiences",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UserExperiences",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "UserSkills",
                keyColumns: new[] { "SkillId", "UserId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "UserSkills",
                keyColumns: new[] { "SkillId", "UserId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "UserSkills",
                keyColumns: new[] { "SkillId", "UserId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "UserSkills",
                keyColumns: new[] { "SkillId", "UserId" },
                keyValues: new object[] { 7, 2 });

            migrationBuilder.DeleteData(
                table: "UserSkills",
                keyColumns: new[] { "SkillId", "UserId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "UserSkills",
                keyColumns: new[] { "SkillId", "UserId" },
                keyValues: new object[] { 7, 3 });

            migrationBuilder.DeleteData(
                table: "UserSkills",
                keyColumns: new[] { "SkillId", "UserId" },
                keyValues: new object[] { 8, 3 });

            migrationBuilder.DeleteData(
                table: "UserSkills",
                keyColumns: new[] { "SkillId", "UserId" },
                keyValues: new object[] { 9, 3 });

            migrationBuilder.DeleteData(
                table: "UserSkills",
                keyColumns: new[] { "SkillId", "UserId" },
                keyValues: new object[] { 5, 4 });

            migrationBuilder.DeleteData(
                table: "UserSkills",
                keyColumns: new[] { "SkillId", "UserId" },
                keyValues: new object[] { 6, 4 });

            migrationBuilder.DeleteData(
                table: "UserSkills",
                keyColumns: new[] { "SkillId", "UserId" },
                keyValues: new object[] { 10, 4 });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Assessments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Assessments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CodingQuestions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CodingQuestions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CodingQuestions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CodingQuestions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CodingQuestions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "HrQuestions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "HrQuestions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "HrQuestions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "HrQuestions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Interviews",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Interviews",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Interviews",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "JobTracks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "JobTracks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "JobTracks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "QuestionTopics",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "QuestionTopics",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "QuestionTopics",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "QuestionTopics",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "QuestionTopics",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "UserCvs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserCvs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserCvs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
