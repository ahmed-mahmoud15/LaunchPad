using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.DTOs.Assessment_Engine;
using Application.Interfaces;
using Domain.Common;

namespace Application.Services
{
    public class AssessmentClient : IAssessmentClient
    {
        private readonly HttpClient http;
        private readonly JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public AssessmentClient(HttpClient http)
        {
            this.http = http;
        }

        public async Task<IEnumerable<ProblemDto>> ListAsync(int easyCount, int mediumCount, int hardCount)
        {
            string url = $"questions?easyCount={easyCount}&mediumCount={mediumCount}&hardCount={hardCount}";
            var result = await http.GetFromJsonAsync<IEnumerable<ProblemDto>>(url, options);
            return result ?? Enumerable.Empty<ProblemDto>();
        }

        public async Task<SubmitResponseDto> SubmitAsync(string slug, SubmitRequestDto request)
        {
            string url = $"questions/{slug}/submissions";

            var response = await http.PostAsJsonAsync(url, request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<SubmitResponseDto>(options);

            return result;
        }
    }
}
