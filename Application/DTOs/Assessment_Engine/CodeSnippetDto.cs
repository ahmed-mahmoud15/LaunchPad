using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs.Assessment_Engine
{
    public class CodeSnippetDto
    {
        [JsonPropertyName("lang")]
        public string Language {  get; set; }
        [JsonPropertyName("langSlug")]
        public string LanguageSlug { get; set; }
        [JsonPropertyName("code")]
        public string Code { get; set; }
    }
}
