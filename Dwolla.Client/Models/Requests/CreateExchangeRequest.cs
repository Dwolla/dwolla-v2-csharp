using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dwolla.Client.Models.Requests
{
    public class CreateExchangeRequest
    {
        [JsonProperty(PropertyName = "_links")]
        public Dictionary<string, Link> Links { get; set; }
        public string Token { get; set; }
        public object Finicity { get; set; }
    }
}