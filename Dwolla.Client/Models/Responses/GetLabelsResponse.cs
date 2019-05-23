using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dwolla.Client.Models.Responses
{
    public class GetLabelsResponse : BaseGetResponse<Label>
    {
        [JsonProperty(PropertyName = "_embedded")]
        public new LabelsEmbed Embedded { get; set; }
    }

    public class LabelsEmbed : Embed<Label>
    {
        public List<Label> Labels { get; set; }

        public override List<Label> Results() => Labels;
    }
}