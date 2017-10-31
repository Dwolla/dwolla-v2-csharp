using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExampleApp.Tasks
{
    [Task("cws", "Create a Webhook Subscription")]
    class WebhookSubscription_Create : BaseTask
    {
        public override async Task Run()
        {
            var rootRes = await Broker.GetRootAsync();
            var createdSubscriptionUri = await Broker.CreateWebhookSubscriptionAsync(
                rootRes.Links["webhook-subscriptions"].Href, $"http://example.com/webhooks/{RandomString(10)}", RandomString(10));

            var subscription = await Broker.GetWebhookSubscriptionAsync(createdSubscriptionUri);
            WriteLine($"Created Subscription {subscription.Id} with url={subscription.Url}");
        }
    }
}
