using System;
using System.Net;
using System.Threading.Tasks;
using Dwolla.Client;
using Dwolla.Client.Models;
using Dwolla.Client.Models.Requests;
using Dwolla.Client.Models.Responses;
using Dwolla.Client.Rest;
using Newtonsoft.Json;

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
            _headers.Add("Authorization", $"Bearer {response.Content.Token}");
            return response.Content;
        }

        public async Task<RootResponse> GetRoot() =>
            (await GetAsync<RootResponse>(new Uri(_client.ApiBaseAddress))).Content;

        public async Task<Uri> CreateCustomerAsync(Uri uri, string firstName, string lastName, string email)
        {
            var response = await PostAsync<CreateCustomerRequest, object>(uri, new CreateCustomerRequest
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email
            });
            return response.Response.Headers.Location;
        }

        public async Task<GetCustomerResponse> GetCustomer(Uri uri) =>
            (await GetAsync<GetCustomerResponse>(uri)).Content;

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
                if (e.Response.StatusCode != HttpStatusCode.Unauthorized) throw;
                try
                {
                    var error = JsonConvert.DeserializeObject<ErrorResponse>(e.Content);
                    if (error.Code == "ExpiredAccessToken")
                    {
                        // TODO: Refresh token and retry request
                    }
                }
                catch (Exception)
                {
                    // Failed to deserialize Dwolla JSON error
                    throw e;
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
    }
}