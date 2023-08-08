using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.WebhookSubscriptions
{
    [Task("lws", "List Webhook Subscriptions")]
    internal class List : BaseTask
    {
        public override async Task Run()
        {
            var response = await HttpService.WebhookSubscriptions.GetWebhookSubscriptionCollectionAsync();

            response.Content.Embedded.WebhookSubscriptions
                .ForEach(ws => WriteLine($"Webhook Subscription: {ws.Id} - {ws.Url}{(ws.Paused ? " PAUSED" : null)}"));
        }
    }
}