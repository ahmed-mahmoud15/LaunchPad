using System.Text.Json.Serialization;

namespace Application.DTOs.Interview
{
    public class InterviewModes
    {
        public static string Behavioral = "behavioral";
        public static string Technical = "technical";
        public static string Resume = "resume";
    }

    public class GenerateQuestionsRequestDto
    {
        [JsonPropertyName("job_description")]
        public string JobDescription {  get; set; }
        [JsonPropertyName("resume")]
        public string? Resume {  get; set; }
        [JsonPropertyName("modes")]
        public List<string> Modes { get; set; } = new List<string>();
        [JsonPropertyName("counts")]
        public Dictionary<string, int> Counts { get; set; } = new Dictionary<string, int>();
    }

    public class SimulatorQuestionDto
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("audio_b64")]
        public string AudioBase64 { get; set; }
    }

    public class GenerateQuestionsResponseDto
    {
        [JsonPropertyName("questions")]
        public List<SimulatorQuestionDto> Questions { get; set; } = new List<SimulatorQuestionDto>();
        [JsonPropertyName("modes_used")]
        public List<string> ModesUsed { get; set; } = new();
    }




    public class SpeechAnalysisDto
    {
        [JsonPropertyName("word_count")]
        public int WordCount { get; set; }

        [JsonPropertyName("duration_seconds")]
        public double DurationSeconds { get; set; }

        [JsonPropertyName("words_per_minute")]
        public double WordsPerMinute { get; set; }

        [JsonPropertyName("filler_word_count")]
        public int FillerWordCount { get; set; }
    }

    public class EvaluationScoresDto
    {
        [JsonPropertyName("technical_accuracy")]
        public int TechnicalAccuracy { get; set; }

        [JsonPropertyName("clarity")]
        public int Clarity { get; set; }

        [JsonPropertyName("completeness")]
        public int Completeness { get; set; }

        [JsonPropertyName("overall_score")]
        public double OverallScore { get; set; }

        [JsonPropertyName("feedback")]
        public string Feedback { get; set; }
    }

    public class EvaluateAnswerResponseDto
    {
        [JsonPropertyName("transcription")]
        public string Transcription { get; set; }

        [JsonPropertyName("speech_analysis")]
        public SpeechAnalysisDto? SpeechAnalysis { get; set; }

        [JsonPropertyName("evaluation")]
        public EvaluationScoresDto? Evaluation { get; set; }
    }
}
