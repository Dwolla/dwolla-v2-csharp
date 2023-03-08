using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dwolla.Client.Models.Responses
{
    public class GetExchangePartnersResponse : BaseGetResponse<ExchangePartnerResponse>
    {
        [JsonPropertyName("_embedded")]
        public new ExchangePartnersEmbed Embedded { get; set; }
    }

    public class ExchangePartnersEmbed : Embed<ExchangePartnerResponse>
    {
        [JsonPropertyName("exchange-partners")]
        public List<ExchangePartnerResponse> ExchangePartners { get; set; }

        public override List<ExchangePartnerResponse> Results() => ExchangePartners;
    }
}