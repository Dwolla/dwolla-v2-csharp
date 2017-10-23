using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dwolla.Client.Models.Responses
{
    public class GetBusinessClassificationsResponse
    {
        [JsonProperty(PropertyName = "_links")]
        public Dictionary<string, Link> Links { get; set; }

        [JsonProperty(PropertyName = "_embedded")]
        public BusinessClassificationsEmbed Embedded { get; set; }

        public int Total { get; set; }
    }

    public class BusinessClassificationsEmbed
    {
        [JsonProperty(PropertyName = "business-classifications")]
        public List<BusinessClassification> BusinessClassifications { get; set; }
    }
}