using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dwolla.Client.Models.Responses
{
    public class GetExchangesResponse : BaseGetResponse<ExchangeResponse>
    {
        [JsonProperty(PropertyName = "_links")]
        public Dictionary<string, Link> Links { get; set; }
        [JsonProperty(PropertyName = "_embedded")]
        public new ExchangesEmbed Embedded { get; set; }
    }

    public class ExchangesEmbed : Embed<ExchangeResponse>
    {
        public List<ExchangeResponse> Exchanges { get; set; }

        public override List<ExchangeResponse> Results() => Exchanges;
    }
}