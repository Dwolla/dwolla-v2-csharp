using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dwolla.Client.Models.Requests
{
    public class CreateMasspaymentRequest
    {
        [JsonProperty(PropertyName = "_links")]
        public Dictionary<string, Link> Links { get; set; }
        public List<MasspaymentItem> Items { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public string Status { get; set; }
        public Clearing Clearing { get; set; }
        public AchDetails AchDetails { get; set; }
        public string CorrelationId { get; set; }
        public string ProcessingChannel { get; set; }
    }
}