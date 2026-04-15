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

        

        public async Task<Result<StartAssessmentDto>> StartAssessmentAsync(int userId, int easyCount, int mediumCount, int hardCount)
        {
            try {
                var problems = await client.ListAsync(easyCount: easyCount, mediumCount: mediumCount, hardCount: hardCount);

                Assessment assesment = new Assessment()
                {
                    CreatedAt = DateTime.UtcNow,
                    StartedAt = DateTime.UtcNow,
                    EasyCount = easyCount,
                    MediumCount = mediumCount,
                    HardCount = hardCount,
                    UserId = userId,
                    Score = 0
                    
                };

                foreach (var problem in problems)
                {
                    var codingQuest = await unit.CodingQuestions.FindAsync(q => q.LeetcodeId == int.Parse(problem.QuestionId));

                    if(codingQuest is null)
                    {
                        codingQuest = new CodingQuestion()
                        {
                            Description = problem.Description,
                            Title = problem.Title,
                            TitleSlug = problem.TitleSlug,
                            LeetcodeId = int.Parse(problem.QuestionId),
                            Difficulty = problem.Difficulty.ToLower() switch
                            {
                                "easy" => QuestionDifficulty.Easy,
                                "medium" => QuestionDifficulty.Medium,
                                "hard" => QuestionDifficulty.Hard,
                                _ => QuestionDifficulty.Easy
                            }

                        };

                        foreach (var tag in problem.TopicTags)
                        {
                            var topic = await unit.QuestionTopics
                                .FindAsync(t => t.Slug == tag.Slug);

                            if (topic is null)
                            {
                                topic = new QuestionTopic
                                {
                                    Name = tag.Name,
                                    Slug = tag.Slug
                                };
                            }

                            codingQuest.CodingQuestionTopics.Add(new CodingQuestionTopic
                            {
                                CodingQuestion = codingQuest,
                                QuestionTopic = topic
                            });
                        }
                    }
                    
                    AssessmentQuestion question = new AssessmentQuestion() {
                        Assessment = assesment,
                        Question = codingQuest,
                        Status = SubmissionStatus.NotAttempted,
                        LanguageUsed = string.Empty
                    };
                    

                    assesment.Questions.Add(question);
                }

                await unit.Assessments.AddAsync(assesment);
                await unit.SaveChangesAsync();

                foreach(var problem in problems)
                {
                    problem.AssessmentQuestionId = assesment.Questions.First(q => q.Question.LeetcodeId == int.Parse(problem.QuestionId)).Id;
                }

                var startDto = new StartAssessmentDto
                {
                    Problems = problems,
                    AssessmentId = assesment.Id
                };

                return Result<StartAssessmentDto>.Ok(startDto);
            }catch (Exception e)
            {
                return Result<StartAssessmentDto>.ServerError($"Failed to Connect with Assessment Engine Server: {e.Message}");
            }
        }

        public async Task<Result<SubmitResponseDto>> SubmitProblemAsync(SubmitRequestDto request, int userId, int assessmentQuestionId)
        {
            var assessmentQuestion = await unit.AssessmentQuestions.GetByIdAsync(assessmentQuestionId);

            if(assessmentQuestion is null)
            {
                return Result<SubmitResponseDto>.NotFound("This question is not found");
            }

            var assessment = await unit.Assessments.GetAssessmentWithIncludesAsync(assessmentQuestion.AssessmentId);

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

            assessmentQuestion.Status = response.StatusCode switch
            {
                10 => SubmissionStatus.Accepted,
                11 => SubmissionStatus.WrongAnswer,
                _ => SubmissionStatus.Error
            };
            assessmentQuestion.CodeSubmitted = request.Code;
            assessmentQuestion.LanguageUsed = response.Lang.Name;
            assessmentQuestion.SubmittedAt = DateTime.UtcNow;
            assessmentQuestion.RunTime = response.Runtime;
            assessmentQuestion.Memory = response.Memory;
            assessmentQuestion.TestCasesPasses = response.TotalCorrect;
            assessmentQuestion.TotalTestCases = response.TotalCorrect;

            await unit.AssessmentQuestions.UpdateAsync(assessmentQuestion);
            await unit.SaveChangesAsync();

            return Result<SubmitResponseDto>.Ok(response);
        }

        public async Task<Result<int>> EndAssessmentAsync(int assessmentId, int userId)
        {
            var assessment = await unit.Assessments.GetAssessmentWithIncludesAsync(assessmentId);

            if (assessment is null ) {
                return Result<int>.NotFound("Assessment not found");
            }

            if (assessment.UserId != userId) {
                return Result<int>.NotFound("Assessment not found");
            }

            if(assessment.CompletedAt is not null)
            {
                return Result<int>.BadRequest("Assessment is alreadt completed");
            }


            int totalWeight = assessment.EasyCount + assessment.MediumCount * 3 + assessment.HardCount * 5;

            int earnedWeight = 0;

            foreach (var question in assessment.Questions) { 
                if(question.Status != SubmissionStatus.Accepted)
                {
                    continue;
                }

                earnedWeight += question.Question.Difficulty switch
                {
                    QuestionDifficulty.Easy => 1,
                    QuestionDifficulty.Medium => 3,
                    QuestionDifficulty.Hard => 5,
                    _ => 1
                };
            }
            int score = (int)Math.Round(100.0 * earnedWeight / totalWeight);
            assessment.Score = score;
            assessment.CompletedAt = DateTime.UtcNow;

            await unit.Assessments.UpdateAsync(assessment);
            await unit.SaveChangesAsync();

            return Result<int>.Ok(score);
        }

        public async Task<Result<SubmitResponseDto>> RunProblemAsync(SubmitRequestDto request, int userId, int assessmentQuestionId)
        {
            var assessmentQuestion = await unit.AssessmentQuestions.GetByIdAsync(assessmentQuestionId);

            if (assessmentQuestion is null)
            {
                return Result<SubmitResponseDto>.NotFound("This question is not found");
            }
            var codingQuestion = await unit.CodingQuestions.GetByIdAsync(assessmentQuestion.QuestionId);

            if (codingQuestion is null)
            {
                return Result<SubmitResponseDto>.NotFound("This coding question is not found");
            }

            var slug = codingQuestion.TitleSlug;


            SubmitResponseDto response;
            try
            {
                response = await client.SubmitAsync(slug, request);
            }
            catch (Exception ex)
            {
                return Result<SubmitResponseDto>.ServerError($"Assessment Engine server is not reachable: {ex.Message}");
            }
            return Result<SubmitResponseDto>.Ok(response);
        }
    }
}
