using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dwolla.Client.Models.Responses
{
    public class BusinessClassification : BaseResponse
    {
        [JsonPropertyName("_embedded")]
        public IndustryClassificationsEmbed Embedded { get; set; }

        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class IndustryClassificationsEmbed : Embed<IndustryClassification>
    {
        [JsonPropertyName("industry-classifications")]
        public List<IndustryClassification> IndustryClassifications { get; set; }

        public override List<IndustryClassification> Results() => IndustryClassifications;
    }

    public class IndustryClassification
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}