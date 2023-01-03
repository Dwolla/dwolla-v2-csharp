using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dwolla.Client.Models.Responses
{
    public class GetWebhookSubscriptionsResponse : BaseGetResponse<WebhookSubscription>
    {
        [JsonPropertyName("_embedded")]
        public new WebhookSubscriptionsEmbed Embedded { get; set; }
    }

    public class WebhookSubscriptionsEmbed : Embed<WebhookSubscription>
    {
        [JsonPropertyName("webhook-subscriptions")]
        public List<WebhookSubscription> WebhookSubscriptions { get; set; }

        public override List<WebhookSubscription> Results() => WebhookSubscriptions;
    }
}