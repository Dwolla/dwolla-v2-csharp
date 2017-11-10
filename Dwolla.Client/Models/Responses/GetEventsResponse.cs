using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dwolla.Client.Models.Responses
{
    public class GetEventsResponse : BaseGetResponse<EventResponse>
    {
        [JsonProperty(PropertyName = "_embedded")]
        public new EventsEmbed Embedded { get; set; }
    }

    public class EventsEmbed : Embed<EventResponse>
    {
        public List<EventResponse> Events { get; set; }

        public override List<EventResponse> Results() => Events;
    }
}