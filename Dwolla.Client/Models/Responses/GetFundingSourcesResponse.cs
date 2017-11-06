using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dwolla.Client.Models.Responses
{
    public class GetFundingSourcesResponse : BaseGetResponse<FundingSource>
    {
        [JsonProperty(PropertyName = "_embedded")]
        public new FundingSourceEmbed Embedded { get; set; }
    }

    public class FundingSourceEmbed : Embed<FundingSource>
    {
        [JsonProperty(PropertyName = "funding-sources")]
        public List<FundingSource> FundingSources { get; set; }

        public override List<FundingSource> Results()
        {
            return FundingSources;
        }
    }
}