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
        public int Id { get; set; }
        [JsonPropertyName("lang")]
        public string Language { get; set; }
        [JsonPropertyName("statusDisplay")]
        public string Status { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("titleSlug")]
        public string Slug { get; set; }

    }
}
