using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dwolla.Client.Models.Responses
{
    public class ErrorResponse : IDwollaResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }

        [JsonPropertyName("_embedded")]
        public ErrorEmbed Embedded { get; set; }
    }

    public class ErrorEmbed
    {
        public List<Error> Errors { get; set; }
    }

    public class Error
    {
        [JsonPropertyName("_links")]
        public Dictionary<string, Link> Links { get; set; }

        public string Code { get; set; }
        public string Message { get; set; }
        public string Path { get; set; }
    }
}