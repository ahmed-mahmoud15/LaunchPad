using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Interview;

namespace Application.Interfaces
{
    public interface IInterviewSimulatorClient
    {
        Task<GenerateQuestionsResponseDto> GenerateQuestionsAsync(
            string jobDescription,
            Dictionary<string, int> modes,
            string? resume
        );
        Task<EvaluateAnswerResponseDto> EvaluateAnswerAsync(
            Stream fileStream,
            string fileName,
            string mimeType,
            string questionText,
            string jobContext
        );
    }
}
