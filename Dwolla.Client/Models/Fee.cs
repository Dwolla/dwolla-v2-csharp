using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dwolla.Client.Models
{
    public class Fee
    {
        [JsonProperty(PropertyName = "_links")]
        public Dictionary<string, Link> Links { get; set; }

        public Money Amount { get; set; }
    }
}