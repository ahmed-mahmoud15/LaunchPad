using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.DTOs.Assessment_Engine
{
    public class ProblemDto
    {
        [JsonPropertyName("questionId")]
        public int QuestionId { get; set; }
        [JsonPropertyName("questionFrontendId")]
        public int FrontEndQuestionId { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("titleSlug")]
        public string TitleSlug { get; set; }
        [JsonPropertyName("content")]
        public string Description { get; set; }
        [JsonPropertyName("difficulty")]
        public string Difficulty { get; set; }
        [JsonPropertyName("exampleTestcases")]
        public string TestCases { get; set; }

        public IEnumerable<string> Tags { get; set; } = new List<string>();

        public int SolutionId { get; set; }
        [JsonPropertyName("hints")]
        public IEnumerable<string> Hints { get; set; } = new List<string>();

        // can use dictionary also
        [JsonPropertyName("codeSnippets")]
        public IEnumerable<CodeSnippetDto> CodeSnippets { get; set; } = new List<CodeSnippetDto>();

    }
}
