using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Interview
{
    public class SubmitAnswerResponseDto
    {
        public string Transcription {  get; set; }
        public SpeechAnalysisDto? SpeechAnalysis { get; set; }
        public EvaluationScoresDto? Evaluation { get; set; }
    }
}
