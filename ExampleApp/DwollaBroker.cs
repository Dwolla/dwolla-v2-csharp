using System;
using System.Threading.Tasks;
using Dwolla.Client;
using Dwolla.Client.Models;
using Dwolla.Client.Models.Requests;
using Dwolla.Client.Models.Responses;
using Dwolla.Client.Rest;
using System.Collections.Generic;

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

        public async Task<Uri> CreateCustomerAsync(Uri uri, string firstName, string lastName, string email)
        {
            return await CreateCustomerAsync(uri, new CreateCustomerRequest
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email
            });
        }

        public async Task<Uri> CreateCustomerAsync(Uri uri, CreateCustomerRequest request)
        {
            var response = await PostAsync(uri, request);
            return response.Response.Headers.Location;
        }

        public async Task<Customer> UpdateCustomerAsync(Uri uri, UpdateCustomerRequest request) =>
            (await PostAsync<UpdateCustomerRequest, Customer>(uri, request)).Content;

        public async Task<Customer> GetCustomerAsync(Uri uri) => (await GetAsync<Customer>(uri)).Content;

        public async Task<GetCustomersResponse> GetCustomersAsync(Uri uri) =>
            (await GetAsync<GetCustomersResponse>(uri)).Content;

        public async Task<GetFundingSourcesResponse> GetCustomerFundingSourcesAsync(Uri customerUri) =>
            (await GetAsync<GetFundingSourcesResponse>(new Uri(customerUri.AbsoluteUri + "/funding-sources"))).Content;

        public async Task<FundingSource> GetFundingSourceAsync(string fundingSourceId) =>
            (await GetAsync<FundingSource>(new Uri($"{_client.ApiBaseAddress}/funding-sources/{fundingSourceId}"))).Content;

        public async Task<BalanceResponse> GetFundingSourceBalanceAsync(Uri balanceUri) =>
            (await GetAsync<BalanceResponse>(balanceUri)).Content;

        public async Task<IavTokenResponse> GetCustomerIavTokenAsync(Uri customerUri) =>
            (await PostAsync<object, IavTokenResponse>(new Uri(customerUri.AbsoluteUri + "/iav-token"), null)).Content;

        public async Task<Uri> CreateTransferAsync(string sourceFundingSourceId, string destinationFundingSourceId, decimal amount, decimal? fee, Uri chargeTo)
        {
            var response = await PostAsync(new Uri($"{_client.ApiBaseAddress}/transfers"),
                new CreateTransferRequest
                {
                    Amount = new Money
                    {
                        Currency = "USD",
                        Value = amount
                    },
                    Links = new Dictionary<string, Link>()
                    {
                        {"source", new Link {Href = new Uri($"{_client.ApiBaseAddress}/funding-sources/{sourceFundingSourceId}")}},
                        {"destination", new Link {Href = new Uri($"{_client.ApiBaseAddress}/funding-sources/{destinationFundingSourceId}")}}
                    },
                    Fees = fee == null || fee == 0m ? null : new List<Fee>()
                    {
                        new Fee
                        {
                            Amount = new Money
                            {
                                Value = fee.Value,
                                Currency = "USD"
                            },
                            Links = new Dictionary<string, Link>()
                            {
                                { "charge-to", new Link() { Href = chargeTo } }
                            }
                        }
                    }

                });
            return response.Response.Headers.Location;
        }

        public async Task<TransferResponse> GetTransferAsync(Uri transferUri) =>
            (await GetAsync<TransferResponse>(transferUri)).Content;

        public async Task<TransferResponse> GetTransferAsync(string id) =>
            (await GetAsync<TransferResponse>(new Uri($"{_client.ApiBaseAddress}/transfers/{id}"))).Content;

        public async Task<Uri> CreateWebhookSubscriptionAsync(Uri uri, string url, string secret)
        {
            var response = await PostAsync(uri,
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

        public async Task<GetEventsResponse> GetEventsAsync(Uri uri) =>
            (await GetAsync<GetEventsResponse>(uri)).Content;

        public async Task<GetBusinessClassificationsResponse> GetBusinessClassificationsAsync() =>
        (await GetAsync<GetBusinessClassificationsResponse>(
            new Uri($"{_client.ApiBaseAddress}/business-classifications"))).Content;

        private async Task<RestResponse<TRes>> GetAsync<TRes>(Uri uri)
            where TRes : IDwollaResponse
        {
            try
            {
                return await _client.GetAsync<TRes>(uri, _headers);
            }
            catch (DwollaException e)
            {
                HandleError(e);

                // Example error handling. More info: https://docsv2.dwolla.com/#errors
                if (e.Error?.Code == "ExpiredAccessToken")
                {
                    // TODO: Refresh token and retry request
                }
                throw;
            }
        }

        private async Task<RestResponse<object>> PostAsync<TReq>(Uri uri, TReq request)
        {
            try
            {
                return await _client.PostAsync(uri, request, _headers);
            }
            catch (DwollaException e)
            {
                HandleError(e);
                throw;
            }
        }

        private async Task<RestResponse<TRes>> PostAsync<TReq, TRes>(Uri uri, TReq request)
            where TRes : IDwollaResponse
        {
            try
            {
                return await _client.PostAsync<TReq, TRes>(uri, request, _headers);
            }
            catch (DwollaException e)
            {
                HandleError(e);
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
                HandleError(e);
                throw;
            }
        }

        private void HandleError(DwollaException e)
        {
            // TODO: Handle error
            Console.WriteLine(e);
        }
    }
}