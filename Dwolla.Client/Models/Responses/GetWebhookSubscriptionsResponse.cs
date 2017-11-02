using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dwolla.Client.Models.Responses
{
    public class GetWebhookSubscriptionsResponse
    {
        [JsonProperty(PropertyName = "_links")]
        public Dictionary<string, Link> Links { get; set; }

        [JsonProperty(PropertyName = "_embedded")]
        public WebhookSubscriptionsEmbed Embedded { get; set; }

        public int Total { get; set; }
    }

    public class WebhookSubscriptionsEmbed
    {
        [JsonProperty(PropertyName = "webhook-subscriptions")]
        public List<WebhookSubscription> WebhookSubscriptions { get; set; }
    }
}