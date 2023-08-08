using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.WebhookSubscriptions
{
    [Task("dws", "Delete a Webhook Subscription")]
    internal class Delete : BaseTask
    {
        public override async Task Run()
        {
            Write("Webhook subscription ID to delete: ");
            var input = ReadLine();

            await HttpService.WebhookSubscriptions.DeleteWebhookSubscriptionAsync(input);

            WriteLine($"Deleted Subscription {input}");
        }
    }
}