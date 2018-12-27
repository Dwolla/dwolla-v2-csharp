using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dwolla.Client;
using Dwolla.Client.Models;
using Dwolla.Client.Models.Requests;
using Dwolla.Client.Models.Responses;
using Dwolla.Client.Rest;

namespace ExampleApp
{
    internal class DwollaBroker
    {
        private readonly Headers _headers = new Headers();
        private readonly IDwollaClient _client;

        internal DwollaBroker(IDwollaClient client) => _client = client;

        internal async Task<TokenResponse> SetAuthorizationHeader(string key, string secret)
        {
            var response = await _client.PostAuthAsync<TokenResponse>(
                new Uri($"{_client.AuthBaseAddress}/token"), new AppTokenRequest {Key = key, Secret = secret});

            // TODO: Securely store token in your database for reuse
            if (!_headers.ContainsKey("Authorization"))
                _headers.Add("Authorization", $"Bearer {response.Content.Token}");
            else
                _headers["Authorization"] = $"Bearer {response.Content.Token}";

            return response.Content;
        }

        internal async Task<RootResponse> GetRootAsync() =>
            (await GetAsync<RootResponse>(new Uri(_client.ApiBaseAddress))).Content;

        internal async Task<Uri> CreateBeneficialOwnerAsync(Uri uri, CreateBeneficialOwnerRequest request)
        {
            var response = await PostAsync(uri, request);
            return response.Response.Headers.Location;
        }

        internal async Task<GetBeneficialOwnersResponse> GetBeneficialOwnersAsync(Uri uri) =>
            (await GetAsync<GetBeneficialOwnersResponse>(uri)).Content;

        internal async Task<BeneficialOwnerResponse> GetBeneficialOwnerAsync(Uri uri) =>
            (await GetAsync<BeneficialOwnerResponse>(uri)).Content;

        internal async Task DeleteBeneficialOwnerAsync(string id) =>
            await DeleteAsync<object>(new Uri($"{_client.ApiBaseAddress}/beneficial-owners/{id}"), null);

        internal async Task<BeneficialOwnershipResponse> GetBeneficialOwnershipAsync(Uri uri) =>
            (await GetAsync<BeneficialOwnershipResponse>(uri)).Content;

        internal async Task<Uri> CertifyBeneficialOwnershipAsync(Uri uri) =>
            (await PostAsync(uri, new CertifyBeneficialOwnershipRequest {Status = "certified"})).Response.Headers.Location;

        internal async Task<Uri> CreateCustomerAsync(Uri uri, string firstName, string lastName, string email) =>
            await CreateCustomerAsync(uri, new CreateCustomerRequest
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email
            });

        internal async Task<Uri> CreateCustomerAsync(Uri uri, CreateCustomerRequest request)
        {
            var r = await PostAsync<CreateCustomerRequest, EmptyResponse>(uri, request);
            return r.Response.Headers.Location;
        }

        internal async Task<Uri> UploadDocumentAsync(Uri uri, UploadDocumentRequest request) =>
            (await ExecAsync(() => _client.UploadAsync(uri, request, _headers))).Response.Headers.Location;

        internal async Task<Customer> UpdateCustomerAsync(Uri uri, UpdateCustomerRequest request) =>
            (await PostAsync<UpdateCustomerRequest, Customer>(uri, request)).Content;

        internal async Task<Customer> GetCustomerAsync(Uri uri) => (await GetAsync<Customer>(uri)).Content;

        internal async Task<GetCustomersResponse> GetCustomersAsync(Uri uri) =>
            (await GetAsync<GetCustomersResponse>(uri)).Content;

        internal async Task<GetDocumentsResponse> GetCustomerDocumentsAsync(Uri customerUri) =>
            (await GetAsync<GetDocumentsResponse>(new Uri(customerUri.AbsoluteUri + "/documents"))).Content;

        internal async Task<GetFundingSourcesResponse> GetCustomerFundingSourcesAsync(Uri customerUri) =>
            (await GetAsync<GetFundingSourcesResponse>(new Uri(customerUri.AbsoluteUri + "/funding-sources"))).Content;

        internal async Task<FundingSource> GetFundingSourceAsync(string fundingSourceId) =>
            (await GetAsync<FundingSource>(new Uri($"{_client.ApiBaseAddress}/funding-sources/{fundingSourceId}"))).Content;

        internal async Task<MicroDepositsResponse> GetMicroDepositsAsync(string fundingSourceId) =>
            (await GetAsync<MicroDepositsResponse>(
                new Uri($"{_client.ApiBaseAddress}/funding-sources/{fundingSourceId}/micro-deposits"))).Content;

        internal async Task<Uri> VerifyMicroDepositsAsync(string fundingSourceId, decimal amount1, decimal amount2) =>
            (await PostAsync(new Uri($"{_client.ApiBaseAddress}/funding-sources/{fundingSourceId}/micro-deposits"),
                new MicroDepositsRequest
                {
                    Amount1 = new Money {Value = amount1, Currency = "USD"},
                    Amount2 = new Money {Value = amount2, Currency = "USD"}
                })).Response.Headers.Location;


        internal async Task<BalanceResponse> GetFundingSourceBalanceAsync(Uri balanceUri) =>
            (await GetAsync<BalanceResponse>(balanceUri)).Content;

        internal async Task<IavTokenResponse> GetCustomerIavTokenAsync(Uri customerUri) =>
            (await PostAsync<EmptyResponse, IavTokenResponse>(new Uri(customerUri.AbsoluteUri + "/iav-token"), null)).Content;

        internal async Task<Uri> CreateTransferAsync(string sourceFundingSourceId, string destinationFundingSourceId,
            decimal amount, decimal? fee, Uri chargeTo)
        {
            var response = await PostAsync(new Uri($"{_client.ApiBaseAddress}/transfers"),
                new CreateTransferRequest
                {
                    Amount = new Money
                    {
                        Currency = "USD",
                        Value = amount
                    },
                    Links = new Dictionary<string, Link>
                    {
                        {"source", new Link {Href = new Uri($"{_client.ApiBaseAddress}/funding-sources/{sourceFundingSourceId}")}},
                        {"destination", new Link {Href = new Uri($"{_client.ApiBaseAddress}/funding-sources/{destinationFundingSourceId}")}}
                    },
                    Fees = fee == null || fee == 0m
                        ? null
                        : new List<Fee>
                        {
                            new Fee
                            {
                                Amount = new Money {Value = fee.Value, Currency = "USD"},
                                Links = new Dictionary<string, Link> {{"charge-to", new Link {Href = chargeTo}}}
                            }
                        }
                });
            return response.Response.Headers.Location;
        }

        internal async Task<TransferResponse> GetTransferAsync(Uri transferUri) =>
            (await GetAsync<TransferResponse>(transferUri)).Content;

        internal async Task<TransferResponse> GetTransferAsync(string id) =>
            (await GetAsync<TransferResponse>(new Uri($"{_client.ApiBaseAddress}/transfers/{id}"))).Content;

        internal async Task<TransferFailureResponse> GetTransferFailureAsync(string id) =>
            (await GetAsync<TransferFailureResponse>(new Uri($"{_client.ApiBaseAddress}/transfers/{id}/failure"))).Content;

        internal async Task<Uri> CreateWebhookSubscriptionAsync(Uri uri, string url, string secret) =>
            (await PostAsync(uri, new CreateWebhookSubscriptionRequest {Url = url, Secret = secret})).Response.Headers.Location;

        internal async Task DeleteWebhookSubscriptionAsync(Uri uri) => await DeleteAsync<object>(uri, null);

        internal async Task<WebhookSubscription> GetWebhookSubscriptionAsync(Uri uri) =>
            (await GetAsync<WebhookSubscription>(uri)).Content;

        internal async Task<GetWebhookSubscriptionsResponse> GetWebhookSubscriptionsAsync(Uri uri) =>
            (await GetAsync<GetWebhookSubscriptionsResponse>(uri)).Content;

        internal async Task<GetEventsResponse> GetEventsAsync(Uri uri) =>
            (await GetAsync<GetEventsResponse>(uri)).Content;

        internal async Task<GetBusinessClassificationsResponse> GetBusinessClassificationsAsync() =>
            (await GetAsync<GetBusinessClassificationsResponse>(
                new Uri($"{_client.ApiBaseAddress}/business-classifications"))).Content;

        private async Task<RestResponse<TRes>> GetAsync<TRes>(Uri uri) where TRes : IDwollaResponse =>
            await ExecAsync(() => _client.GetAsync<TRes>(uri, _headers));

        private async Task<RestResponse<EmptyResponse>> PostAsync<TReq>(Uri uri, TReq request) =>
            await ExecAsync(() => _client.PostAsync<TReq, EmptyResponse>(uri, request, _headers));

        private async Task<RestResponse<TRes>> PostAsync<TReq, TRes>(Uri uri, TReq request) where TRes : IDwollaResponse =>
            await ExecAsync(() => _client.PostAsync<TReq, TRes>(uri, request, _headers));

        private async Task<RestResponse<EmptyResponse>> DeleteAsync<TReq>(Uri uri, TReq request) =>
            await ExecAsync(() => _client.DeleteAsync(uri, request, _headers));

        private static async Task<RestResponse<TRes>> ExecAsync<TRes>(Func<Task<RestResponse<TRes>>> func) where TRes : IDwollaResponse
        {
            var r = await func();
            if (r.Error == null) return r;

            // TODO: Handle error specific to your application
            var e = r.Error;
            Console.WriteLine($"{e.Code}: {e.Message}");

            // Example error handling. More info: https://docsv2.dwolla.com/#errors
            if (e.Code == "ExpiredAccessToken")
            {
                // TODO: Refresh token and retry request
            }

            return r;
        }
    }
}