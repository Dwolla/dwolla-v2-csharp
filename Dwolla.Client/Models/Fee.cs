using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dwolla.Client.Models
{
    public class Fee
    {
        [JsonProperty(PropertyName = "_links")]
        public Dictionary<string, Link> Links { get; set; }

        public Money Amount { get; set; }
    }
}