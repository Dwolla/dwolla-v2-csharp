using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dwolla.Client.Models.Responses
{
    public class GetFundingSourcesResponse : BaseGetResponse<FundingSource>
    {
        [JsonPropertyName("_embedded")]
        public new FundingSourceEmbed Embedded { get; set; }
    }

    public class FundingSourceEmbed : Embed<FundingSource>
    {
        [JsonPropertyName("funding-sources")]
        public List<FundingSource> FundingSources { get; set; }

        public override List<FundingSource> Results() => FundingSources;
    }
}