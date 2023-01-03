using System;
using System.Text.Json.Serialization;

namespace Dwolla.Client.Models
{
    public class Link
    {
        public Uri Href { get; set; }
        public string Type { get; set; }

        [JsonPropertyName("resource-type")]
        public string ResourceType { get; set; }
    }
}