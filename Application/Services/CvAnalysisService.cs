using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Cv;
using Application.DTOs.CvAnalyzer;
using Application.Interfaces;
using Application.Services.Cloudinary;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class CvAnalysisService : ICvAnalysisService
    {
        private readonly IUnitOfWork unit;
        private readonly ICvAnalyzerClient cvAnalyzerClient;
        private readonly IStorageService storage;
        private readonly IUserCvService userCvService;

        public CvAnalysisService(IUnitOfWork unit, ICvAnalyzerClient cvClient, IStorageService storage, IUserCvService userCvService)
        {
            this.unit = unit;
            this.cvAnalyzerClient = cvClient;
            this.storage = storage;
            this.userCvService = userCvService;
        }

        public async Task<Result<AnalyzeCvResultDto>> AnalyzeCvAsync(int userId, AnalyzeCvRequestDto dto)
        {
            int cvId;
            UserCv cv;
            if (dto.CvId.HasValue)
            {
                cv = await unit.UserCvs.FindAsync(c => c.Id == dto.CvId && c.UserId == userId);
                if (cv is null)
                {
                    return Result<AnalyzeCvResultDto>.NotFound("Cv not found or does not belong to you");
                }

                cvId = cv.Id;
            }
            else
            {
                var uploadDto = new UploadCvDto
                {
                    File = dto.File
                };
                var uploadResult = await userCvService.UploadCvAsync(userId, uploadDto);
                if (!uploadResult.IsSuccess)
                {
                    return Result<AnalyzeCvResultDto>.BadRequest(uploadResult.ErrorMessage);
                }
                cvId = uploadResult.Value;

                cv = await unit.UserCvs.GetByIdAsync(cvId);
            }


            int jobId;
            Job job;

            if (dto.JobId.HasValue)
            {
                job = await unit.Jobs.FindAsync(j => j.Id == dto.JobId && j.UserId == userId);

                if (job is null)
                {
                    return Result<AnalyzeCvResultDto>.NotFound("job not found or does not belong to you");
                }
                jobId = job.Id;
            }
            else
            {
                job = new Job
                {
                    UserId = userId,
                    CvId = cvId,
                    Title = dto.JobTitle,
                    Info = dto.JobDescription,
                    Type = Domain.Enums.JobType.FullTime,
                };

                await unit.Jobs.AddAsync(job);
                await unit.SaveChangesAsync();

                jobId = job.Id;
            }


            Stream pdfStream;
            try
            {
                pdfStream = await storage.DownloadAsync(cv.FilePath);
            }
            catch (Exception ex)
            {
                return Result<AnalyzeCvResultDto>.ServerError($"Failed to retrieve cv file: {ex.Message}");
            }

            var jobDescription = job.Info;
            var jobTitle = job.Title;

            CvAnalyzerResponseDto response;
            try
            {
                using (pdfStream)
                {
                    response = await cvAnalyzerClient.EvaluateAsync(pdfStream, cv.FileName, jobDescription, jobTitle);
                }
            }
            catch (Exception ex)
            {
                return Result<AnalyzeCvResultDto>.ServerError($"Cv analyzer service is unavailable : {ex.Message}");
            }

            var feedback = BuildFeedback(response);

            var analysis = new CvJobAnalysis
            {
                UserId = userId,
                CvId = cvId,
                JobId = jobId,
                Score = (int)Math.Round(response.JobMatchResults?.FinalScore ?? 0),
                Feedback = feedback,
                AnalyzeDate = DateTime.UtcNow
            };

            await unit.CvJobAnalyses.AddAsync(analysis);

            if (response.CvAnalysisResults is not null)
            {
                cv.Score = (int)Math.Round(response.CvAnalysisResults.TotalScore);
                await unit.UserCvs.UpdateAsync(cv);
            }

            await unit.SaveChangesAsync();

            return Result<AnalyzeCvResultDto>.Created(MapToDto(analysis.Id, response));
        }

        private string BuildFeedback(CvAnalyzerResponseDto response)
        {
            var parts = new List<string>();
            var matchScore = response.JobMatchResults?.FinalScore ?? 0;
            var analysisScore = response.CvAnalysisResults?.TotalScore ?? 0;

            parts.Add($"Job match Score: {matchScore:F1}/100. CV analysis score: {analysisScore:F1}/100.");

            var tech = response.JobMatchResults?.SkillsSummary?.TechnicalSkills;

            if (tech != null)
            {
                if(tech.Covered.Count > 0)
                {
                    parts.Add($"Technical Skills covered: {string.Join(", ", tech.Covered)}.");
                }

                if (tech.Missing.Count > 0)
                {
                    parts.Add($"Technical Skills missing: {string.Join(", ", tech.Missing)}.");
                }
            }

            var soft = response.JobMatchResults?.SkillsSummary?.SoftSkills;
            if (soft != null) {
                if (soft.Covered.Count > 0)
                {
                    parts.Add($"Soft Skills covered: {string.Join(", ", soft.Covered)}.");
                }

                if (soft.Missing.Count > 0)
                {
                    parts.Add($"Soft Skills missing: {string.Join(", ", soft.Missing)}.");
                }
            }

            return string.Join( " ", parts );
        }

        private AnalyzeCvResultDto MapToDto(int analysisId, CvAnalyzerResponseDto response)
        {
            var jobMatch = response.JobMatchResults;
            var cvAnalysis = response.CvAnalysisResults;
            var extractedData = response.ExtractedData;

            var techSummary = jobMatch?.SkillsSummary?.TechnicalSkills;
            var softSummary = jobMatch?.SkillsSummary?.SoftSkills;

            var techBreakdown = jobMatch?.Breakdown?.TechnicalSkills;
            var softBreakdown = jobMatch?.Breakdown?.SoftSkills;

            var ats = cvAnalysis?.Breakdown?.AtsCompliance;
            var language = cvAnalysis?.Breakdown?.LinguisticPrecision;

            return new AnalyzeCvResultDto
            {
                AnalysisId = analysisId,

                JobMatchScore = jobMatch?.FinalScore ?? 0,

                OverallSimilarity = jobMatch?.OverallSimilarity ?? 0,

                CvQualityScore = cvAnalysis?.TotalScore ?? 0,

                Feedback = BuildFeedback(response),

                Grammar = extractedData?.GrammarAnalysis == null
                    ? null
                    : new GrammarAnalysisDto
                    {
                        ErrorCount = extractedData.GrammarAnalysis.ErrorCount,
                        TenseConsistent = extractedData.GrammarAnalysis.TenseConsistent,
                        OverallAssessment = extractedData.GrammarAnalysis.OverallAssessment
                    },

                TechnicalSkills = new SkillCoverageDto
                {
                    Covered = techSummary?.Covered ?? new(),
                    Missing = techSummary?.Missing ?? new(),
                    Coverage = techBreakdown?.Coverage ?? 0,
                    ExactRatio = techBreakdown?.ExactRatio ?? 0
                },

                SoftSkills = new SkillCoverageDto
                {
                    Covered = softSummary?.Covered ?? new(),
                    Missing = softSummary?.Missing ?? new(),
                    Coverage = softBreakdown?.Coverage ?? 0,
                    ExactRatio = softBreakdown?.ExactRatio ?? 0
                },

                Ats = ats is null
                    ? null
                    : new AtsComplianceDto
                    {
                        Score = ats.RawScore,
                        IsSingleColumn = ats.Details?.Details?.Layout?.IsSingleColumn ?? false,
                        UsesStandardFont = ats.Details?.Details?.Typography?.IsStandard ?? false,
                        DetectedFont = ats.Details?.Details?.Typography?.DetectedFont,
                        GoodDateFormatsFound = ats.Details?.Details?.DateFormat?.GoodFormatsFound ?? 0
                    },

                Language = language is null
                    ? null
                    : new LinguisticPrecisionDto
                    {
                        Score = language.RawScore,
                        HighImpactVerbCount = language.Details?.Details?.Verbs?.HighImpactCount ?? 0,
                        WeakVerbCount = language.Details?.Details?.Verbs?.WeakVerbCount ?? 0,
                        AverageWordsPerBullet = language.Details?.Details?.Brevity?.AvgWordsPerBullet ?? 0,
                        BulletsOverTwentyWords = language.Details?.Details?.Brevity?.BulletsOver20Words ?? 0
                    }
            };
        }
    }
}
