using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.DTOs.Interview;
using Application.Interfaces;

namespace Application.Services
{
    public class InterviewSimulatorClient : IInterviewSimulatorClient
    {
        private readonly HttpClient http;
        private static readonly JsonSerializerOptions Options = new()
        {
            PropertyNameCaseInsensitive = true,
        };

        public InterviewSimulatorClient(HttpClient http)
        {
            this.http = http;
        }
        public async Task<EvaluateAnswerResponseDto> EvaluateAnswerAsync(Stream fileStream, string fileName, string mimeType, string questionText, string jobContext)
        {
            using var form = new MultipartFormDataContent();

            var fileContent = new StreamContent(fileStream);
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(mimeType);

            form.Add(fileContent, "file", fileName);
            form.Add(new StringContent(questionText), "question");
            form.Add(new StringContent(jobContext), "context");

            var response = await http.PostAsync("evaluate_answer", form);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<EvaluateAnswerResponseDto>(json, Options) ?? throw new InvalidOperationException("Null response rom interview Simulator");
        }

        public async Task<GenerateQuestionsResponseDto> GenerateQuestionsAsync(string jobDescription, Dictionary<string, int> modes, string? resume)
        {
            var payload = new GenerateQuestionsRequestDto
            {
                JobDescription = jobDescription,
                Counts = modes,
                Resume = resume
            };
            var content = new StringContent(JsonSerializer.Serialize(payload, Options), Encoding.UTF8, "application/json");

            var response = await http.PostAsync("generate_questions", content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GenerateQuestionsResponseDto>(json, Options) ?? throw new InvalidOperationException("Null response from Interview Simulator");
        }
    }
}
