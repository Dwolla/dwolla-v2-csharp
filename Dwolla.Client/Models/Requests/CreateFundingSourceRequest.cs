using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dwolla.Client.Models.Requests
{
    public class CreateFundingSourceRequest
    {
        [JsonPropertyName("_links")]
        public Dictionary<string, Link> Links { get; set; }
        public string RoutingNumber { get; set; }
        public string AccountNumber { get; set; }
        public string BankAccountType { get; set; }
        public string Name { get; set; }
    }
}