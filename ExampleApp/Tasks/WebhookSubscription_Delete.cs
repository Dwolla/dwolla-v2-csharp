using System;
using System.Threading.Tasks;

namespace ExampleApp.Tasks
{
    [Task("dws", "Delete a Webhook Subscription")]
    class WebhookSubscription_Delete : BaseTask
    {
        public override async Task Run()
        {
            Write("Webhook subscription ID to delete: ");
            var input = ReadLine();

            var rootRes = await Broker.GetRootAsync();
            await Broker.DeleteWebhookSubscriptionAsync(
                new Uri(rootRes.Links["webhook-subscriptions"].Href + "/" + input));
            WriteLine($"Deleted Subscription {input}");
        }
    }
}