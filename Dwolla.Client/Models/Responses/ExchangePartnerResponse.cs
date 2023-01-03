using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dwolla.Client.Models.Responses
{
    public class ExchangePartnerResponse : BaseResponse
    {
        [JsonPropertyName("_links")]
        public Dictionary<string, Link> Links { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }

    }
}