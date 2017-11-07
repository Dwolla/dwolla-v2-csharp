using System.Threading.Tasks;

namespace ExampleApp.Tasks.WebhookSubscriptions
{
    [Task("lws", "List Webhook Subscriptions")]
    class List : BaseTask
    {
        public override async Task Run()
        {
            var rootRes = await Broker.GetRootAsync();
            var res = await Broker.GetWebhookSubscriptionsAsync(rootRes.Links["webhook-subscriptions"].Href);
            res.Embedded.WebhookSubscriptions
                .ForEach(ws => WriteLine($" - {ws.Id}: {ws.Url}{(ws.Paused ? " PAUSED" : null)}"));
        }
    }
}