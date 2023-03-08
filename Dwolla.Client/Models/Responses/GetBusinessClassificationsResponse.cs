using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dwolla.Client.Models.Responses
{
    public class GetBusinessClassificationsResponse : BaseGetResponse<BusinessClassification>
    {
        [JsonPropertyName("_embedded")]
        public new BusinessClassificationsEmbed Embedded { get; set; }
    }

    public class BusinessClassificationsEmbed : Embed<BusinessClassification>
    {
        [JsonPropertyName("business-classifications")]
        public List<BusinessClassification> BusinessClassifications { get; set; }

        public override List<BusinessClassification> Results() => BusinessClassifications;
    }
}