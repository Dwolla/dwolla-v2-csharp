using Dwolla.Client.HttpServices.Architecture;
using Dwolla.Client.Models.Requests;
using Dwolla.Client.Models.Responses;
using Dwolla.Client.Rest;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dwolla.Client.HttpServices
{
    public class WebhookSubscriptionsHttpService : BaseHttpService
    {
        public WebhookSubscriptionsHttpService(IDwollaClient dwollaClient, Func<Task<string>> getAccessToken)
           : base(dwollaClient, getAccessToken)
        {
        }

        public async Task<RestResponse<WebhookSubscription>> GetWebhookSubscriptionAsync(string webhookSubscriptionId, CancellationToken cancellation = default)
        {
            if (string.IsNullOrWhiteSpace(webhookSubscriptionId))
            {
                throw new ArgumentException("WebhookSubscriptionId should not be blank.");
            }

            return await GetAsync<WebhookSubscription>(new Uri($"{client.ApiBaseAddress}/webhook-subscriptions/{webhookSubscriptionId}"), cancellation);
        }

        public async Task<RestResponse<GetWebhookSubscriptionsResponse>> GetWebhookSubscriptionCollectionAsync(CancellationToken cancellation = default)
        {
            return await GetAsync<GetWebhookSubscriptionsResponse>(new Uri($"{client.ApiBaseAddress}/webhook-subscriptions"), cancellation);
        }

        public async Task<RestResponse<EmptyResponse>> CreateWebhookSubscriptionAsync(CreateWebhookSubscriptionRequest request, string idempotencyKey = null, CancellationToken cancellationToken = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            return await PostAsync<CreateWebhookSubscriptionRequest, EmptyResponse>(new Uri($"{client.ApiBaseAddress}/webhook-subscriptions"), request, idempotencyKey, cancellationToken);
        }

        public async Task<RestResponse<EmptyResponse>> DeleteWebhookSubscriptionAsync(string webhookSubscriptionId)
        {
            if (string.IsNullOrWhiteSpace(webhookSubscriptionId))
            {
                throw new ArgumentException("WebhookSubscriptionId should not be blank.");
            }

            return await DeleteAsync<EmptyResponse>(new Uri($"{client.ApiBaseAddress}/webhook-subscriptions/{webhookSubscriptionId}"), null);
        }
    }
}
