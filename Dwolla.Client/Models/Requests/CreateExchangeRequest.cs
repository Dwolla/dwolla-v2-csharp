using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dwolla.Client.Models.Requests
{
    public class CreateExchangeRequest
    {
        [JsonPropertyName("_links")]
        public Dictionary<string, Link> Links { get; set; }
        public string Token { get; set; }
        public object Finicity { get; set; }
    }
}