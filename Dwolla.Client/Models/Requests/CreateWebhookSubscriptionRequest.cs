namespace Dwolla.Client.Models.Requests
{
    public class CreateWebhookSubscriptionRequest
    {
        public string Url { get; set; }
        public string Secret { get; set; }
    }
}