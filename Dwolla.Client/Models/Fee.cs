using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dwolla.Client.Models
{
    public class Fee
    {
        [JsonPropertyName("_links")]
        public Dictionary<string, Link> Links { get; set; }

        public Money Amount { get; set; }
    }
}