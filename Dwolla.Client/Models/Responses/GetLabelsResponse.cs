using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dwolla.Client.Models.Responses
{
    public class GetLabelsResponse : BaseGetResponse<Label>
    {
        [JsonPropertyName("_embedded")]
        public new LabelsEmbed Embedded { get; set; }
    }

    public class LabelsEmbed : Embed<Label>
    {
        public List<Label> Labels { get; set; }

        public override List<Label> Results() => Labels;
    }
}