using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dwolla.Client.Models.Responses
{
    public class GetExchangesResponse : BaseGetResponse<ExchangeResponse>
    {
        [JsonPropertyName("_links")]
        public Dictionary<string, Link> Links { get; set; }
        [JsonPropertyName("_embedded")]
        public new ExchangesEmbed Embedded { get; set; }
    }

    public class ExchangesEmbed : Embed<ExchangeResponse>
    {
        public List<ExchangeResponse> Exchanges { get; set; }

        public override List<ExchangeResponse> Results() => Exchanges;
    }
}