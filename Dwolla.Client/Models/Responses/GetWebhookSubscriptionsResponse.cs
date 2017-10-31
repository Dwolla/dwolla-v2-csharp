using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dwolla.Client.Models.Responses
{
    public class GetWebhookSubscriptionsResponse : BaseGetResponse<WebhookSubscription>
    {
        [JsonProperty(PropertyName = "_embedded")]
        public new WebhookSubscriptionsEmbed Embedded { get; set; }
    }

    public class WebhookSubscriptionsEmbed : Embed<WebhookSubscription>
    {
        [JsonProperty(PropertyName = "webhook-subscriptions")]
        public List<WebhookSubscription> WebhookSubscriptions { get; set; }

        public override List<WebhookSubscription> Results()
        {
            return WebhookSubscriptions;
        }
    }
}