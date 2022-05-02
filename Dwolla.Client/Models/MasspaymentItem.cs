using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dwolla.Client.Models
{
    public class MasspaymentItem
    {
        [JsonProperty(PropertyName = "_links")]
        public Dictionary<string, Link> Links { get; set; }
        public Money Amount { get; set; }
        public string CorrelationId { get; set; }
        public Clearing Clearing { get; set; }
        public AchDetails AchDetails { get; set; }
        public ProcessingChannel ProcessingChannel { get; set; }
        public Dictionary<string, string> Metadata { get; set; }

    }
}