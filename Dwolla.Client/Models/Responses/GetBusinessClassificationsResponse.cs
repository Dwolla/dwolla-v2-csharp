using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dwolla.Client.Models.Responses
{
    public class GetBusinessClassificationsResponse : BaseGetResponse<BusinessClassification>
    {
        [JsonProperty(PropertyName = "_embedded")]
        public new BusinessClassificationsEmbed Embedded { get; set; }
    }

    public class BusinessClassificationsEmbed : Embed<BusinessClassification>
    {
        [JsonProperty(PropertyName = "business-classifications")]
        public List<BusinessClassification> BusinessClassifications { get; set; }

        public override List<BusinessClassification> Results()
        {
            return BusinessClassifications;
        }
    }
}