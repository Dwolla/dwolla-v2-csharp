using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dwolla.Client.Models.Requests
{
    public class CreatePlaidFundingSourceRequest
    {
        [JsonPropertyName("_links")]
        public Dictionary<string, Link> Links { get; set; }
        public string Name { get; set; }
        public string PlaidToken { get; set; }
    }

}