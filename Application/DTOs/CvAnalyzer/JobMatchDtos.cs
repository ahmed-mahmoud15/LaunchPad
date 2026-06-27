using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Application.DTOs.Cv;

namespace Application.DTOs.CvAnalyzer
{
    public class JobMatchResultsDto
    {
        [JsonPropertyName("final_score")]
        public double FinalScore { get; set; }
        [JsonPropertyName("skills_summary")]
        public SkillsSummaryDto? SkillsSummary { get; set; }
        [JsonPropertyName("overall_similarity")]
        public double OverallSimilarity { get; set; }
        [JsonPropertyName("breakdown")]
        public JobMatchBreakdownDto? Breakdown { get; set; }
    }
    public class SkillsSummaryDto
    {
        [JsonPropertyName("technical_skills")]
        public SkillGroupDto TechnicalSkills { get; set; }
        [JsonPropertyName("soft_skills")]
        public SkillGroupDto SoftSkills { get; set; }
    }
    public class SkillGroupDto
    {
        [JsonPropertyName("covered")]
        public List<string> Covered { get; set; } = new List<string>();
        [JsonPropertyName("missing")]
        public List<string> Missing { get; set; } = new List<string>();
    }
    public class JobMatchBreakdownDto
    {
        [JsonPropertyName("technical_skills")]
        public CoverageBreakdownDto? TechnicalSkills { get; set; }

        [JsonPropertyName("soft_skills")]
        public CoverageBreakdownDto? SoftSkills { get; set; }

        [JsonPropertyName("education")]
        public CoverageBreakdownDto? Education { get; set; }
    }

    public class CoverageBreakdownDto
    {
        [JsonPropertyName("coverage")]
        public double Coverage { get; set; }

        [JsonPropertyName("exact_ratio")]
        public double ExactRatio { get; set; }
    }
}
