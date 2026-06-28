using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Interview;
using Application.Interfaces;
using Application.Services.Cloudinary;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class InterviewService : IInterviewService
    {
        private readonly IUnitOfWork unit;
        private readonly IInterviewSimulatorClient simulator;
        private readonly IPdfParser pdfParser;
        private readonly IStorageService storage;

        public InterviewService(IUnitOfWork unit, IInterviewSimulatorClient simulator, IPdfParser pdfParser, IStorageService storage)
        {
            this.unit = unit;
            this.simulator = simulator;
            this.pdfParser = pdfParser;
            this.storage = storage;
        }

        public async Task<Result<StartInterviewResponseDto>> StartInterviewAsync(int userId, StartInterviewRequestDto dto)
        {
            string jobDescription;

            if (dto.JobId.HasValue)
            {
                var job = await unit.Jobs.FindAsync(j => j.Id == dto.JobId && j.UserId == userId);

                if(job is null)
                {
                    return Result<StartInterviewResponseDto>.NotFound("Job is not found or not belong to you");
                }
                jobDescription = job.Info ?? job.Title;
            }else if (!string.IsNullOrWhiteSpace(dto.JobDescription))
            {
                jobDescription = dto.JobDescription;
            }
            else
            {
                return Result<StartInterviewResponseDto>.BadRequest("You have to provide job description or valid job id");
            }

            string? resume = null;
            if (dto.CvId.HasValue)
            {
                var cv = await unit.UserCvs.FindAsync(c => c.Id == dto.CvId && c.UserId == userId);
                if(cv is null)
                {
                    return Result<StartInterviewResponseDto>.NotFound("Cv is not found or not belong to you");
                }

                var pdfStream = await storage.DownloadAsync(cv.FilePath);
                resume = await pdfParser.ExtractTextAsync(pdfStream);

            }else if(dto.File is not null)
            {
                await using var stream = dto.File.OpenReadStream();
                resume = await pdfParser.ExtractTextAsync(stream);
            }

            GenerateQuestionsResponseDto questionsGenerated;

            try
            {
                Dictionary<string, int> modes = new Dictionary<string, int>();
                if(dto.BehavioralCount > 0)
                {
                    modes[InterviewModes.Behavioral] = dto.BehavioralCount;
                }
                if(dto.TechnicalCount > 0)
                {
                    modes[InterviewModes.Technical] = dto.TechnicalCount;
                }
                if(dto.ResumeCount > 0)
                {
                    modes[InterviewModes.Resume] = dto.ResumeCount;
                }
                
                questionsGenerated = await simulator.GenerateQuestionsAsync(jobDescription, modes, resume);

            }
            catch (Exception ex) {
                return Result<StartInterviewResponseDto>.ServerError($"Interview Simulator service is unreachable: {ex.Message}");
            }

            if(questionsGenerated.Questions.Count == 0)
            {
                return Result<StartInterviewResponseDto>.ServerError("Interview Simulator returned no questions.");
            }


            var interview = new Interview
            {
                UserId = userId,
                StartedAt = DateTime.UtcNow,
                Score = 0,
                
            };

            foreach (var q in questionsGenerated.Questions) {
                var hrQuestion = new HrQuestion
                {
                    Question = q.Text,
                    AudioQuestion = q.AudioBase64
                };
                interview.InterviewQuestions.Add(new InterviewQuestion
                {
                    Interview = interview,
                    Question = hrQuestion,
                    UserResponse = string.Empty
                });
            }

            await unit.Interviews.AddAsync(interview);
            await unit.SaveChangesAsync();

            var savedQuestions = interview.InterviewQuestions.ToList();

            var questionItems = questionsGenerated.Questions.Select((q, i) => new InterviewQuestionItemDto
            {
                AudioBase64 = q.AudioBase64,
                InterviewQuestionId = savedQuestions[i].Id,
                QuestionText = q.Text
            }).ToList();

            return Result<StartInterviewResponseDto>.Created(new StartInterviewResponseDto
            {
                InterviewId = interview.Id,
                Questions = questionItems
            });
        }

        public async Task<Result<SubmitAnswerResponseDto>> SubmitAnswerAsync(int userId, int interviewQuestionId, IFormFile file)
        {
            var interviewQuestion = await unit.InterviewQuestions.GetByIdAsync(interviewQuestionId);

            if(interviewQuestion is null)
            {
                return Result<SubmitAnswerResponseDto>.NotFound("Interview Question is not found");
            }

            var interview = await unit.Interviews.GetByIdAsync(interviewQuestion.InterviewId);

            if(interview is null || interview.UserId != userId)
            {
                return Result<SubmitAnswerResponseDto>.Forbidden("You are not allowed to access this interview.");
            }

            if (interview.EndedAt.HasValue)
            {
                return Result<SubmitAnswerResponseDto>.BadRequest("This interview has been ended");
            }

            var hrQuestion = await unit.HrQuestions.GetByIdAsync(interviewQuestion.QuestionId);

            if (hrQuestion is null)
            {
                return Result<SubmitAnswerResponseDto>.NotFound("Question not found");
            }

            EvaluateAnswerResponseDto evaluation;

            try
            {
                using var stream = file.OpenReadStream();
                evaluation = await simulator.EvaluateAnswerAsync(
                    stream,
                    file.FileName,
                    file.ContentType ?? "application/octet-stream",
                    hrQuestion.Question,
                    jobContext: null
                );
            }
            catch (Exception ex) {
                return Result<SubmitAnswerResponseDto>.ServerError($"Interview Simulator service is not reachable; {ex.Message}");
            }

            interviewQuestion.UserResponse = evaluation.Transcription ?? string.Empty;
            interviewQuestion.Feedback = evaluation.Evaluation?.Feedback;
            interviewQuestion.Score = evaluation.Evaluation?.OverallScore;

            await unit.InterviewQuestions.UpdateAsync(interviewQuestion);
            await unit.SaveChangesAsync();

            return Result<SubmitAnswerResponseDto>.Ok(new SubmitAnswerResponseDto
            {
                Transcription = evaluation.Transcription,
                SpeechAnalysis = evaluation.SpeechAnalysis,
                Evaluation = evaluation.Evaluation
            });
        }

        public async Task<Result<int>> EndInterviewAsync(int userId, int interviewId)
        {
            var interview = await unit.Interviews.GetByIdAsync(interviewId);

            if (interview is null || interview.UserId != userId)
            {
                return Result<int>.NotFound("interview is not found");
            }
            if (interview.EndedAt.HasValue)
            {
                return Result<int>.BadRequest("Interview is alredy ended");
            }

            var questions = await unit.InterviewQuestions.FindAllAsync(q => q.InterviewId == interviewId);

            var answered = questions.Where(q => q.Score.HasValue).ToList();

            int finalScore = answered.Count > 0 ? (int)Math.Round(answered.Average(q => q.Score!.Value) * 10) : 0;

            interview.Score = finalScore;
            interview.EndedAt = DateTime.UtcNow;

            await unit.Interviews.UpdateAsync(interview);
            await unit.SaveChangesAsync();

            return Result<int>.Ok(finalScore);
        }

        public async Task<Result<InterviewDetailsDto>> GetInterviewDetailsAsync(int userId, int interviewId)
        {
            var interview = await unit.Interviews.GetByIdAsync(interviewId);

            if (interview is null || interview.UserId != userId)
            {
                return Result<InterviewDetailsDto>.NotFound("Interview is not found");
            }

            var questions = (await unit.InterviewQuestions.FindAllAsync(q => q.InterviewId == interviewId)).ToList();


            var hrQuestionIds = questions.Select(q => q.QuestionId).ToList();
            var hrQuestions = await unit.HrQuestions.FindAllAsync(h => hrQuestionIds.Contains(h.Id));

            var hrDictionary = hrQuestions.ToDictionary(h => h.Id);

            var questionDtos = questions.Select(q => new InterviewQuestionDetailsDto
            {
                Id = q.Id,
                QuestionText = hrDictionary.TryGetValue(q.QuestionId, out var hr) ? hr.Question : string.Empty,
                UserResponse = q.UserResponse,
                Feedback = q.Feedback,
                Score = q.Score,
                QuestionAudio = q.Question.AudioQuestion
            }).ToList();

            return Result<InterviewDetailsDto>.Ok(new InterviewDetailsDto
            {
                Id = interviewId,
                StartedAt = interview.StartedAt,
                EndedAt = interview.EndedAt,
                Score = interview.Score,
                Questions = questionDtos
            });
        }

    }
}
