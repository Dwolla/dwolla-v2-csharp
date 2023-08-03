using System.Linq;
using System.Threading.Tasks;
using Dwolla.Client.Models.Requests;

namespace ExampleApp.HttpServices.Tasks.WebhookSubscriptions
{
    [Task("cws", "Create a Webhook Subscription")]
    internal class Create : BaseTask
    {
        public override async Task Run()
        {
            var response = await HttpService.WebhookSubscriptions.CreateWebhookSubscriptionAsync(
                new CreateWebhookSubscriptionRequest { Url = $"http://example.com/webhooks/{RandomAlphaNumericString(10)}", Secret = RandomAlphaNumericString(10) });

            var subscriptionResponse = await HttpService.WebhookSubscriptions.GetWebhookSubscriptionAsync(response.Response.Headers.Location.ToString().Split('/').Last());

            WriteLine($"Created Subscription: {subscriptionResponse.Content.Id} | Url - {subscriptionResponse.Content.Url}");
        }
    }
}