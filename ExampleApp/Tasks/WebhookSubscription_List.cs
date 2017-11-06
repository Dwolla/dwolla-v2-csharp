using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExampleApp.Tasks
{
    [Task("lws", "List Webhook Subscriptions")]
    class WebhookSubscription_List : BaseTask
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
