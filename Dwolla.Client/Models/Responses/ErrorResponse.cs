using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dwolla.Client.Models.Responses
{
    public class ErrorResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }

        [JsonProperty(PropertyName = "_embedded")]
        public Embedded Embedded { get; set; }
    }

    public class Embedded
    {
        public List<Error> Errors { get; set; }
    }

    public class Error
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string Path { get; set; }

        [JsonProperty(PropertyName = "_links")]
        public Dictionary<string, Link> Links { get; set; }
    }
}