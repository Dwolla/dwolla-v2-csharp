using System;
using System.Threading.Tasks;
using Dwolla.Client;
using Dwolla.Client.Models;
using Dwolla.Client.Models.Requests;
using Dwolla.Client.Models.Responses;
using Dwolla.Client.Rest;

namespace ExampleApp
{
    public class DwollaBroker
    {
        private readonly Headers _headers = new Headers();
        private readonly IDwollaClient _client;

        public DwollaBroker(IDwollaClient client) => _client = client;

        public async Task<TokenResponse> SetAuthroizationHeader(string key, string secret)
        {
            var response = await _client.PostAuthAsync<AppTokenRequest, TokenResponse>(
                new Uri($"{_client.AuthBaseAddress}/token"), new AppTokenRequest {Key = key, Secret = secret});

            // TODO: Securely store token in database for reuse
            if (!_headers.ContainsKey("Authorization"))
                _headers.Add("Authorization", $"Bearer {response.Content.Token}");
            else
                _headers["Authorization"] = $"Bearer {response.Content.Token}";

            return response.Content;
        }

        public async Task<RootResponse> GetRootAsync() =>
            (await GetAsync<RootResponse>(new Uri(_client.ApiBaseAddress))).Content;

        public async Task<Uri> CreateCustomerAsync(Uri uri, string firstName, string lastName, string email, DateTime? dateOfBirth = null)
        {
            return await CreateCustomerAsync(uri, 
                new CreateCustomerRequest
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    DateOfBirth = dateOfBirth
                });
        }

        public async Task<Uri> CreateCustomerAsync(Uri uri, CreateCustomerRequest request)
        {
            var response = await PostAsync<CreateCustomerRequest, object>(uri, request);
            return response.Response.Headers.Location;
        }

        public async Task<Customer> GetCustomerAsync(Uri uri) => (await GetAsync<Customer>(uri)).Content;

        public async Task<GetCustomersResponse> GetCustomersAsync(Uri uri) => (await GetAsync<GetCustomersResponse>(uri)).Content;

        public async Task<GetFundingSourcesResponse> GetCustomerFundingSourcesAsync(Uri customerUri) => 
            (await GetAsync<GetFundingSourcesResponse>(new Uri(customerUri.AbsoluteUri + "/funding-sources"))).Content;

        public async Task<Uri> CreateWebhookSubscriptionAsync(Uri uri, string url, string secret)
        {
            var response = await PostAsync<CreateWebhookSubscriptionRequest, object>(uri,
                new CreateWebhookSubscriptionRequest
                {
                    Url = url,
                    Secret = secret
                });
            return response.Response.Headers.Location;
        }

        public async Task DeleteWebhookSubscriptionAsync(Uri uri)
        {
            await DeleteAsync<object>(uri, null);
        }

        public async Task<WebhookSubscription> GetWebhookSubscriptionAsync(Uri uri) =>
            (await GetAsync<WebhookSubscription>(uri)).Content;

        public async Task<GetWebhookSubscriptionsResponse> GetWebhookSubscriptionsAsync(Uri uri) =>
            (await GetAsync<GetWebhookSubscriptionsResponse>(uri)).Content;

        public async Task<GetBusinessClassificationsResponse> GetBusinessClassificationsAsync() =>
        (await GetAsync<GetBusinessClassificationsResponse>(
            new Uri($"{_client.ApiBaseAddress}/business-classifications"))).Content;

        private async Task<RestResponse<TRes>> GetAsync<TRes>(Uri uri)
        {
            try
            {
                return await _client.GetAsync<TRes>(uri, _headers);
            }
            catch (DwollaException e)
            {
                Console.WriteLine(e);

                // Example error handling. More info: https://docsv2.dwolla.com/#errors
                if (e.Error?.Code == "ExpiredAccessToken")
                {
                    // TODO: Refresh token and retry request
                }
                throw;
            }
        }

        private async Task<RestResponse<TRes>> PostAsync<TReq, TRes>(Uri uri, TReq request)
        {
            try
            {
                return await _client.PostAsync<TReq, TRes>(uri, request, _headers);
            }
            catch (DwollaException e)
            {
                // TODO: Handle error
                Console.WriteLine(e);
                throw;
            }
        }

        private async Task<RestResponse<object>> DeleteAsync<TReq>(Uri uri, TReq request)
        {
            try
            {
                return await _client.DeleteAsync(uri, request, _headers);
            }
            catch (DwollaException e)
            {
                // TODO: Handle error
                Console.WriteLine(e);
                throw;
            }
        }
    }
}