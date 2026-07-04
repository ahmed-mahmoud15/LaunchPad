using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Application.DTOs.Cv;

namespace Application.DTOs.CvAnalyzer
{
    public class CvAnalysisResultsDto
    {
        [JsonPropertyName("total_score")]
        public double TotalScore { get; set; }

        [JsonPropertyName("breakdown")]
        public CvAnalysisBreakdownDto? Breakdown { get; set; }
    }
    public class CvAnalysisBreakdownDto
    {
        [JsonPropertyName("w4_ats_compliance")]
        public AtsComplianceSectionDto? AtsCompliance { get; set; }

        [JsonPropertyName("w5_linguistic_precision")]
        public LinguisticPrecisionSectionDto? LinguisticPrecision { get; set; }
    }
    public class VerbsDto
    {
        [JsonPropertyName("high_impact_count")]
        public int HighImpactCount { get; set; }

        [JsonPropertyName("weak_verb_count")]
        public int WeakVerbCount { get; set; }
    }
    public class BrevityDto
    {
        [JsonPropertyName("avg_words_per_bullet")]
        public double AvgWordsPerBullet { get; set; }

        [JsonPropertyName("bullets_over_20_words")]
        public int BulletsOver20Words { get; set; }
    }
    public class AtsComplianceSectionDto
    {
        [JsonPropertyName("raw_score")]
        public double RawScore { get; set; }

        [JsonPropertyName("details")]
        public AtsDetailsDto2? Details { get; set; }
    }
    public class AtsDetailsDto2
    {
        [JsonPropertyName("details")]
        public AtsDetailsDto? Details { get; set; }
    }
    public class AtsDetailsDto
    {
        [JsonPropertyName("layout")]
        public AtsLayoutDto? Layout { get; set; }

        [JsonPropertyName("typography")]
        public AtsTypographyDto? Typography { get; set; }

        [JsonPropertyName("date_format")]
        public AtsDateFormatDto? DateFormat { get; set; }
    }
    public class AtsLayoutDto
    {
        [JsonPropertyName("is_single_column")]
        public bool IsSingleColumn { get; set; }
    }
    public class AtsTypographyDto
    {
        [JsonPropertyName("is_standard")]
        public bool IsStandard { get; set; }

        [JsonPropertyName("detected_font")]
        public string? DetectedFont { get; set; }
    }
    public class AtsDateFormatDto
    {
        [JsonPropertyName("good_formats_found")]
        public int GoodFormatsFound { get; set; }
    }

    public class LinguisticPrecisionSectionDto
    {
        [JsonPropertyName("raw_score")]
        public double RawScore { get; set; }

        [JsonPropertyName("details")]
        public LinguisticDetailsDto2? Details { get; set; }
    }
    public class LinguisticDetailsDto2
    {
        [JsonPropertyName("details")]
        public LinguisticDetailsDto? Details { get; set; }
    }
    public class LinguisticDetailsDto
    {
        [JsonPropertyName("verbs")]
        public VerbsDto? Verbs { get; set; }

        [JsonPropertyName("brevity")]
        public BrevityDto? Brevity { get; set; }
    }
}
