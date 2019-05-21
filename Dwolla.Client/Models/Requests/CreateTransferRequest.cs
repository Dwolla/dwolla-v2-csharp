using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dwolla.Client.Models.Requests
{
    public class CreateTransferRequest
    {
        [JsonProperty(PropertyName = "_links")]
        public Dictionary<string, Link> Links { get; set; }

        public Money Amount { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public Clearing Clearing { get; set; }
        public List<Fee> Fees { get; set; }
        public AchDetails AchDetails { get; set; }
        public string CorrelationId { get; set; }
    }
}