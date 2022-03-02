using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dwolla.Client.Models.Requests
{
    public class CreatePlaidFundingSourceRequest
    {
        [JsonProperty(PropertyName = "_links")]
        public Dictionary<string, Link> Links { get; set; }
        public string Name { get; set; }
        public string PlaidToken { get; set; }
    }

}