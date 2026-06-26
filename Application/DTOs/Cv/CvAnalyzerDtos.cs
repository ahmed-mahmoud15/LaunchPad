using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Validations;
using Microsoft.AspNetCore.Http;

namespace Application.DTOs.Cv
{
    public class CvAnalyzerResponseDto
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("job_match_results")]
        public JobMatchResultsDto JobMatchResults { get; set; }
        [JsonPropertyName("cv_analysis_results")]
        public CvAnalysisScoresDto CvAnalysisResults { get; set; }
    }

    public class JobMatchResultsDto
    {
        [JsonPropertyName("final_score")]
        public double FinalScore { get; set; }
        [JsonPropertyName("skills_summary")]
        public SkillsSummaryDto SkillsSummary { get; set; }
    }
    
    public class SkillsSummaryDto
    {
        [JsonPropertyName("technical_skills")]
        public SkillGroupDto TechnicalSkills { get; set; }
        [JsonPropertyName("soft_skills")]
        public SkillGroupDto SoftSkills { get; set; }
    }
    public class SkillGroupDto {
        [JsonPropertyName("covered")]
        public List<string> Covered { get; set; } = new List<string>();
        [JsonPropertyName("missing")]
        public List<string> Missing { get; set; } = new List<string>();
    }

    public class CvAnalysisScoresDto
    {
        [JsonPropertyName("total_score")]
        public double TotalScore { get; set; }
    }



    [ValidCv]
    [ValidJob]
    public class AnalyzeCvRequestDto : ICvRequest
    {
        public int? CvId { get; set; }
        public IFormFile? File { get; set; }
        public int? JobId { get; set; }
        public string? JobDescription { get; set; }
        public string? JobTitle { get; set; }
    }

    public class AnalyzeCvResultDto
    {
        public int AnalysisId { get; set;}
        public double JobMatchScore {  get; set;}
        public double CvQualityScore { get; set;}
        public string Feedback { get; set; }
        public List<string> TechnicalSkillsCovered { get; set; } = new();
        public List<string> TechnicalSkillsMissing { get; set; } = new();
        public List<string> SoftSkillsCovered { get; set; } = new();
        public List<string> SoftSkillsMissing { get; set; } = new();
    }
}
