using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs.Assessment_Engine
{
    public class SubmitRequestDto
    {
        [JsonPropertyName("lang")]
        public string Language { get; set; }
        [JsonPropertyName("questionId")]
        public int LeetcodeId { get; set; }
        [JsonPropertyName("typedCode")]
        public string Code { get; set; }
    }
}
