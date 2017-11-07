using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dwolla.Client.Models.Responses
{
    public class BusinessClassification : BaseResponse
    {
        [JsonProperty(PropertyName = "_embedded")]
        public IndustryClassificationsEmbed Embedded { get; set; }

        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class IndustryClassificationsEmbed : Embed<IndustryClassification>
    {
        [JsonProperty(PropertyName = "industry-classifications")]
        public List<IndustryClassification> IndustryClassifications { get; set; }

        public override List<IndustryClassification> Results() => IndustryClassifications;
    }

    public class IndustryClassification
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}