using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Assessment_Engine;
using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;

namespace Application.Services
{
    public class AssessmentService : IAssessmentService
    {

        private readonly IAssessmentClient client;
        private readonly IUnitOfWork unit;

        public AssessmentService(IAssessmentClient client, IUnitOfWork unit)
        {
            this.client = client;
            this.unit = unit;
        }

        public async Task<Result<IEnumerable<ProblemDto>>> GetProblemsAsync(int userId, int easyCount, int mediumCount, int hardCount)
        {
            try {
                var problems = await client.ListAsync(easyCount: easyCount, mediumCount: mediumCount, hardCount: hardCount);

                Assessment assesment = new Assessment()
                {
                    CreatedAt = DateTime.Now,
                    EasyCount = easyCount,
                    MediumCount = mediumCount,
                    HardCount = hardCount,
                    UserId = userId,
                    Score = 0
                    
                };

                foreach (var problem in problems)
                {
                    var codingQuest = await unit.CodingQuestions.FindAsync(q => q.LeetcodeId == problem.QuestionId);
                    if(codingQuest is null)
                    {
                        codingQuest = new CodingQuestion()
                        {
                            Description = problem.Description,
                            Title = problem.Title,
                            TitleSlug = problem.TitleSlug,
                            LeetcodeId = problem.QuestionId,
                            Difficulty = problem.Difficulty.ToLower() switch
                            {
                                "easy" => QuestionDifficulty.Easy,
                                "medium" => QuestionDifficulty.Medium,
                                "hard" => QuestionDifficulty.Hard,
                                _ => QuestionDifficulty.Easy
                            }

                        };
                    }
                    
                    AssessmentQuestion question = new AssessmentQuestion() {
                        Assessment = assesment,
                        Question = codingQuest,
                        Status = SubmissionStatus.NotAttempted
                    };
                    assesment.Questions.Add(question);
                }

                await unit.Assessments.AddAsync(assesment);
                await unit.SaveChangesAsync();

                return Result<IEnumerable<ProblemDto>>.Ok(problems);
            }catch (Exception e)
            {
                return Result<IEnumerable<ProblemDto>>.ServerError($"Failed to Connect with Assessment Engine Server: {e.Message}");
            }
        }

        public async Task<Result<SubmitResponseDto>> SubmitProblemAsync(SubmitRequestDto request, int userId, int assessmentQuestionId)
        {
            var assessmentQuestion = await unit.AssessmentQuestions.GetByIdAsync(assessmentQuestionId);

            if(assessmentQuestion is null)
            {
                return Result<SubmitResponseDto>.NotFound("This question is not found");
            }

            var assessment = await unit.Assessments.GetByIdAsync(assessmentQuestion.AssessmentId);

            if(assessment is null || assessment.UserId != userId)
            {
                return Result<SubmitResponseDto>.Forbidden("You don't have access to this assessment");
            }

            var codingQuestion = await unit.CodingQuestions.GetByIdAsync(assessmentQuestion.QuestionId);

            if(codingQuestion is null)
            {
                return Result<SubmitResponseDto>.NotFound("This coding question is not found");
            }

            var slug = codingQuestion.TitleSlug;


            SubmitResponseDto response;
            try
            {
                response = await client.SubmitAsync(slug, request);
            }catch(Exception ex)
            {
                return Result<SubmitResponseDto>.ServerError($"Assessment Engine server is not reachable: {ex.Message}");
            }

            assessmentQuestion.Status = response.Status;
            assessmentQuestion.CodeSubmitted = request.typedCode;
            assessmentQuestion.LanguageUsed = request.lang;
            assessmentQuestion.SubmittedAt = DateTime.UtcNow;

            await unit.AssessmentQuestions.UpdateAsync(assessmentQuestion);
            await unit.SaveChangesAsync();

            return Result<SubmitResponseDto>.Ok(response);
        }
    }
}
