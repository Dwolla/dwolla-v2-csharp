using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Dwolla.Client.Models;
using Dwolla.Client.Rest;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

[assembly: InternalsVisibleTo("Dwolla.Client.Tests")]

namespace Dwolla.Client
{
    public interface IDwollaClient
    {
        string ApiBaseAddress { get; }
        string AuthBaseAddress { get; }

        Task<RestResponse<TRes>> PostAuthAsync<TReq, TRes>(Uri uri, TReq content);
        Task<RestResponse<TRes>> GetAsync<TRes>(Uri uri, Headers headers);
        Task<RestResponse<TRes>> PostAsync<TReq, TRes>(Uri uri, TReq content, Headers headers);
    }

    public class DwollaClient : IDwollaClient
    {
        public const string ContentType = "application/vnd.dwolla.v1.hal+json";
        public string ApiBaseAddress { get; }
        public string AuthBaseAddress { get; }

        public static readonly JsonSerializerSettings JsonSettings =
            new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()};

        private const string AuthContentType = "application/json";

        private readonly IRestClient _client;

        public static DwollaClient Create(bool isSandbox) =>
            new DwollaClient(new RestClient(CreateHttpClient()), isSandbox);

        public async Task<RestResponse<TRes>> PostAuthAsync<TReq, TRes>(Uri uri, TReq content) =>
            await SendAsync<TRes>(CreatePostRequest(uri, content, new Headers(), AuthContentType));

        public async Task<RestResponse<TRes>> GetAsync<TRes>(Uri uri, Headers headers) =>
            await SendAsync<TRes>(CreateRequest(HttpMethod.Get, uri, headers));

        public async Task<RestResponse<TRes>> PostAsync<TReq, TRes>(Uri uri, TReq content, Headers headers) =>
            await SendAsync<TRes>(CreatePostRequest(uri, content, headers));

        private async Task<RestResponse<TRes>> SendAsync<TRes>(HttpRequestMessage request)
        {
            var response = await _client.SendAsync<TRes>(request);
            if (response.Exception != null) throw CreateException(response);
            return response;
        }

        private static HttpRequestMessage CreatePostRequest<TReq>(
            Uri requestUri, TReq content, Headers headers, string contentType = ContentType)
        {
            var r = CreateRequest(HttpMethod.Post, requestUri, headers);
            r.Content = new StringContent(
                JsonConvert.SerializeObject(content, JsonSettings), Encoding.UTF8, contentType);
            return r;
        }

        private static HttpRequestMessage CreateRequest(HttpMethod method, Uri requestUri, Headers headers)
        {
            var r = new HttpRequestMessage(method, requestUri);
            foreach (var header in headers) r.Headers.Add(header.Key, header.Value);
            return r;
        }

        private static DwollaException CreateException<T>(RestResponse<T> response) =>
            new DwollaException(response.Response, response.Exception);

        internal DwollaClient(IRestClient client, bool isSandbox)
        {
            _client = client;
            ApiBaseAddress = isSandbox ? "https://api-sandbox.dwolla.com" : "https://api.dwolla.com";
            AuthBaseAddress = isSandbox ? "https://sandbox.dwolla.com/oauth/v2" : "https://www.dwolla.com/oauth/v2";
        }

        internal static HttpClient CreateHttpClient()
        {
            var client = new HttpClient(new HttpClientHandler {SslProtocols = SslProtocols.Tls12});
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("dwolla-v2-csharp", "1.0.0"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));
            return client;
        }
    }
}