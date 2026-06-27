using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.DTOs.CvAnalyzer;
using Application.Interfaces;

namespace Application.Services
{
    public class CvAnalyzerClient : ICvAnalyzerClient
    {
        private readonly HttpClient http;
        private static readonly JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true,
        };

        public CvAnalyzerClient(HttpClient http)
        {
            this.http = http;
        }

        public async Task<CvAnalyzerResponseDto> EvaluateAsync(Stream pdfStream, string fileName, string jobDescription)
        {
            using var form = new MultipartFormDataContent();
            form.Add(new StreamContent(pdfStream), "cv_file", fileName);
            form.Add(new StringContent(jobDescription), "job_description");

            var response = await http.PostAsync("api/v1/evaluate", form);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CvAnalyzerResponseDto>(json, options) ?? throw new InvalidOperationException("Recieved null responce from Cv analyzer service");
        }
    }
}
