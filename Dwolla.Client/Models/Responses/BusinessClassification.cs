using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dwolla.Client.Models.Responses
{
    public class BusinessClassification
    {
        [JsonProperty(PropertyName = "_links")]
        public Dictionary<string, Link> Links { get; set; }

        [JsonProperty(PropertyName = "_embedded")]
        public IndustryClassificationsEmbed Embedded { get; set; }

        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class IndustryClassificationsEmbed
    {
        [JsonProperty(PropertyName = "industry-classifications")]
        public List<IndustryClassification> IndustryClassifications { get; set; }
    }
}