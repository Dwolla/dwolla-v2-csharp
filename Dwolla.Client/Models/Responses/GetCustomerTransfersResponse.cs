using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dwolla.Client.Models.Responses
{
    public class GetCustomerTransfersResponse : BaseGetResponse<EmptyResponse>
    {
        [JsonPropertyName("_embedded")]
        public new TransfersEmbed Embedded { get; set; }
    }

    public class TransfersEmbed : Embed<TransferResponse>
    {
        public List<TransferResponse> Transfers { get; set; }

        public override List<TransferResponse> Results() => Transfers;
    }
}