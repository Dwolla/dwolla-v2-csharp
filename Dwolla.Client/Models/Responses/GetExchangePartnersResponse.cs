using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dwolla.Client.Models.Responses
{
    public class GetExchangePartnersResponse : BaseGetResponse<ExchangePartnerResponse>
    {
        [JsonProperty(PropertyName = "_links")]
        public Dictionary<string, Link> Links { get; set; }
        [JsonProperty(PropertyName = "_embedded")]
        public new ExchangePartnersEmbed Embedded { get; set; }
    }

    public class ExchangePartnersEmbed : Embed<ExchangePartnerResponse>
    {
        [JsonProperty(PropertyName = "exchange-partners")]
        public List<ExchangePartnerResponse> ExchangePartners { get; set; }

        public override List<ExchangePartnerResponse> Results() => ExchangePartners;
    }
}