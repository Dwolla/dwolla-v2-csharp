using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dwolla.Client.Models.Responses
{
    public class GetBeneficialOwnersResponse : BaseGetResponse<BeneficialOwnerResponse>
    {
        [JsonPropertyName("_embedded")]
        public new BeneficialOwnersEmbed Embedded { get; set; }
    }

    public class BeneficialOwnersEmbed : Embed<BeneficialOwnerResponse>
    {
        [JsonPropertyName("beneficial-owners")]
        public List<BeneficialOwnerResponse> BeneficialOwners { get; set; }

        public override List<BeneficialOwnerResponse> Results() => BeneficialOwners;
    }
}