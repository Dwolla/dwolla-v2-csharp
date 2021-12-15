using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dwolla.Client.Models.Requests
{
    public class CreatePlaidFundingSourceRequest
    {
        public string Name { get; set; }
        public string PlaidToken { get; set; }
    }

    public class CreateFundingSourceRequest
    {
        [JsonProperty(PropertyName = "_links")]
        public Dictionary<string, Link> Links { get; set; }

        public string RoutingNumber { get; set; }
        public string AccountNumber { get; set; }
        public string BankAccountType { get; set; }
        public string Name { get; set; }
        public List<string> Channels { get; set; }
    }
}