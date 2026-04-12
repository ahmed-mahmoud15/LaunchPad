using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.DTOs.Assessment_Engine
{

    /*
     sample output

            {
                "id": 1969725292,
                "lang": "csharp",
                "time": "0 minutes",
                "timestamp": 1775400881000,
                "statusDisplay": "Accepted",
                "runtime": 1,
                "url": "/submissions/detail/1969725292/",
                "isPending": false,
                "title": "Add Binary",
                "memory": 41.1,
                "titleSlug": "add-binary"
            }
     */

    public class SubmitResponseDto
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("runtime")]
        public int Runtime { get; set; }

        [JsonPropertyName("runtimeDisplay")]
        public string RuntimeDisplay { get; set; }
        [JsonPropertyName("memory")]
        public long Memory { get; set; }

        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }

        [JsonPropertyName("memoryDisplay")]
        public string MemoryDisplay { get; set; }
        [JsonPropertyName("totalCorrect")]
        public int TotalCorrect { get; set; }

        [JsonPropertyName("totalTestcases")]
        public int TotalTestcases { get; set; }

        [JsonPropertyName("lang")]
        public LanguageDto Lang { get; set; }

        [JsonPropertyName("question")]
        public QuestionDto Question { get; set; }
    }
    public class LanguageDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("verboseName")]
        public string VerboseName { get; set; }
    }
    public class QuestionDto
    {
        [JsonPropertyName("questionId")]
        public string QuestionId { get; set; }

        [JsonPropertyName("titleSlug")]
        public string TitleSlug { get; set; }

        [JsonPropertyName("hasFrontendPreview")]
        public bool HasFrontendPreview { get; set; }
    }
}
