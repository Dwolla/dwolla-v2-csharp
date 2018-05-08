using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dwolla.Client.Models.Responses
{
    public class GetBeneficialOwnersResponse : BaseGetResponse<BeneficialOwnerResponse>
    {
        [JsonProperty(PropertyName = "_embedded")]
        public new BeneficialOwnersEmbed Embedded { get; set; }
    }

    public class BeneficialOwnersEmbed : Embed<BeneficialOwnerResponse>
    {
        [JsonProperty(PropertyName = "beneficial-owners")]
        public List<BeneficialOwnerResponse> BeneficialOwners { get; set; }

        public override List<BeneficialOwnerResponse> Results() => BeneficialOwners;
    }
}