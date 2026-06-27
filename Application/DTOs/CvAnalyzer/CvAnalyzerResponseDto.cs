using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Application.DTOs.Cv;

namespace Application.DTOs.CvAnalyzer
{
    public class CvAnalyzerResponseDto
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("job_match_results")]
        public JobMatchResultsDto? JobMatchResults { get; set; }
        [JsonPropertyName("cv_analysis_results")]
        public CvAnalysisResultsDto? CvAnalysisResults { get; set; }
        [JsonPropertyName("extracted_data")]
        public ExtractedDataDto? ExtractedData { get; set; }
    }

    public class ExtractedDataDto
    {
        [JsonPropertyName("grammar_analysis")]
        public GrammarAnalysisDto? GrammarAnalysis { get; set; }
    }
}
