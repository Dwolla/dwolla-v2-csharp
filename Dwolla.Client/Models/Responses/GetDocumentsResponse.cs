using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dwolla.Client.Models.Responses
{
    public class GetDocumentsResponse : BaseGetResponse<DocumentResponse>
    {
        [JsonPropertyName("_embedded")]
        public new DocumentsEmbed Embedded { get; set; }
    }

    public class DocumentsEmbed : Embed<DocumentResponse>
    {
        public List<DocumentResponse> Documents { get; set; }

        public override List<DocumentResponse> Results() => Documents;
    }
}