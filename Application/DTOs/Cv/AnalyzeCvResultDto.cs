using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs.Cv
{
    public class AnalyzeCvResultDto
    {
        public int AnalysisId { get; set; }

        public double JobMatchScore { get; set; }

        public double OverallSimilarity { get; set; }

        public double CvQualityScore { get; set; }

        public string Feedback { get; set; } = string.Empty;

        public SkillCoverageDto TechnicalSkills { get; set; } = new();

        public SkillCoverageDto SoftSkills { get; set; } = new();

        public GrammarAnalysisDto? Grammar { get; set; }

        public AtsComplianceDto? Ats { get; set; }

        public LinguisticPrecisionDto? Language { get; set; }
    }
    public class SkillCoverageDto
    {
        public List<string> Covered { get; set; } = new();

        public List<string> Missing { get; set; } = new();

        public double Coverage { get; set; }

        public double ExactRatio { get; set; }
    }
    public class GrammarAnalysisDto
    {
        [JsonPropertyName("error_count")]
        public int ErrorCount { get; set; }

        [JsonPropertyName("tense_consistent")]
        public bool TenseConsistent { get; set; }

        [JsonPropertyName("overall_assessment")]
        public string? OverallAssessment { get; set; }
    }

    public class AtsComplianceDto
    {
        public double Score { get; set; }

        public bool IsSingleColumn { get; set; }

        public bool UsesStandardFont { get; set; }

        public string? DetectedFont { get; set; }

        public int GoodDateFormatsFound { get; set; }
    }
    public class LinguisticPrecisionDto
    {
        public double Score { get; set; }

        public int HighImpactVerbCount { get; set; }

        public int WeakVerbCount { get; set; }

        public double AverageWordsPerBullet { get; set; }

        public int BulletsOverTwentyWords { get; set; }
    }
}
