using System.Threading.Tasks;

namespace ExampleApp.Tasks.WebhookSubscriptions
{
    [Task("cws", "Create a Webhook Subscription")]
    class Create : BaseTask
    {
        public override async Task Run()
        {
            var rootRes = await Broker.GetRootAsync();
            var createdSubscriptionUri = await Broker.CreateWebhookSubscriptionAsync(
                rootRes.Links["webhook-subscriptions"].Href,
                $"http://example.com/webhooks/{RandomAlphaNumericString(10)}",
                RandomAlphaNumericString(10));

            var subscription = await Broker.GetWebhookSubscriptionAsync(createdSubscriptionUri);
            WriteLine($"Created Subscription {subscription.Id} with url={subscription.Url}");
        }
    }
}