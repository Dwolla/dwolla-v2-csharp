using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Dwolla.Client.Models;
using Dwolla.Client.Models.Responses;
using Dwolla.Client.Rest;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

[assembly: InternalsVisibleTo("Dwolla.Client.Tests")]

namespace Dwolla.Client
{
    public interface IDwollaClient
    {
        string ApiBaseAddress { get; }
        string AuthBaseAddress { get; }

        Task<RestResponse<TRes>> PostAuthAsync<TReq, TRes>(Uri uri, TReq content) where TRes : IDwollaResponse;
        Task<RestResponse<TRes>> GetAsync<TRes>(Uri uri, Headers headers) where TRes : IDwollaResponse;
        Task<RestResponse<TRes>> PostAsync<TReq, TRes>(Uri uri, TReq content, Headers headers) where TRes : IDwollaResponse;
        Task<RestResponse<object>> PostAsync<TReq>(Uri uri, TReq content, Headers headers);
        Task<RestResponse<object>> DeleteAsync<TReq>(Uri uri, TReq content, Headers headers);
    }

    public class DwollaClient : IDwollaClient
    {
        public const string ContentType = "application/vnd.dwolla.v1.hal+json";
        public string ApiBaseAddress { get; }
        public string AuthBaseAddress { get; }

        private static readonly JsonSerializerSettings JsonSettings =
            new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()};

        private const string AuthContentType = "application/json";

        private readonly IRestClient _client;

        public static DwollaClient Create(bool isSandbox) =>
            new DwollaClient(new RestClient(CreateHttpClient()), isSandbox);

        public async Task<RestResponse<TRes>> PostAuthAsync<TReq, TRes>(Uri uri, TReq content) where TRes : IDwollaResponse =>
            await SendAsync<TRes>(CreatePostRequest(uri, content, new Headers(), AuthContentType));

        public async Task<RestResponse<TRes>> GetAsync<TRes>(Uri uri, Headers headers) where TRes : IDwollaResponse =>
            await SendAsync<TRes>(CreateRequest(HttpMethod.Get, uri, headers));
        
        /// <summary>
        /// Send a post request to <paramref name="uri"/> containing <paramref name="content"/>. 
        /// </summary>
        /// <typeparam name="TReq">Type of the request content</typeparam>
        /// <typeparam name="TRes">Type of the response content</typeparam>
        /// <param name="uri">The resource to which to Post</param>
        /// <param name="content">The content to send</param>
        /// <param name="headers">Request headers</param>
        /// <returns>RestResponse of deserialized content object</returns>
        public async Task<RestResponse<TRes>> PostAsync<TReq, TRes>(Uri uri, TReq content, Headers headers) where TRes : IDwollaResponse =>
            await SendAsync<TRes>(CreatePostRequest(uri, content, headers));

        /// <summary>
        /// Send a post request to <paramref name="uri"/> containing <paramref name="content"/>. 
        /// </summary>
        /// <typeparam name="TReq">Type of the request content</typeparam>
        /// <param name="uri">The resource to which to Post</param>
        /// <param name="content">The content to send</param>
        /// <param name="headers">Request headers</param>
        /// <returns>RestResponse of non-deserialized content object</returns>
        public async Task<RestResponse<object>> PostAsync<TReq>(Uri uri, TReq content, Headers headers) =>
            await SendAsync<object>(CreatePostRequest(uri, content, headers));

        public async Task<RestResponse<object>> DeleteAsync<TReq>(Uri uri, TReq content, Headers headers) =>
            await SendAsync<object>(CreateDeleteRequest(uri, content, headers));

        private async Task<RestResponse<TRes>> SendAsync<TRes>(HttpRequestMessage request)
        {
            var r = await _client.SendAsync<TRes>(request);
            if (r.Exception == null) return r;

            var e = CreateException(r);
            try
            {
                e.Error = JsonConvert.DeserializeObject<ErrorResponse>(r.Exception.Content);
            }
            catch (Exception)
            {
                throw e;
            }
            throw e;
        }

        private static HttpRequestMessage CreateDeleteRequest<TReq>(
            Uri requestUri, TReq content, Headers headers, string contentType = ContentType)
        {
            return CreateContentRequest(HttpMethod.Delete, requestUri, headers, content, contentType);
        }

        private static HttpRequestMessage CreatePostRequest<TReq>(
            Uri requestUri, TReq content, Headers headers, string contentType = ContentType)
        {
            return CreateContentRequest(HttpMethod.Post, requestUri, headers, content, contentType);
        }

        private static HttpRequestMessage CreateContentRequest<TReq>(HttpMethod method, Uri requestUri, Headers headers,
            TReq content, string contentType)
        {
            var r = CreateRequest(method, requestUri, headers);
            r.Content = content != null
                ? new StringContent(JsonConvert.SerializeObject(content, JsonSettings), Encoding.UTF8, contentType)
                : null;
            return r;
        }

        private static HttpRequestMessage CreateRequest(HttpMethod method, Uri requestUri, Headers headers)
        {
            var r = new HttpRequestMessage(method, requestUri);
            foreach (var header in headers)
                r.Headers.Add(header.Key, header.Value);
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

        internal static string ClientVersion = typeof(DwollaClient).GetTypeInfo().Assembly
            .GetCustomAttribute<AssemblyFileVersionAttribute>().Version;

        internal static HttpClient CreateHttpClient()
        {
            var client = new HttpClient(new HttpClientHandler {SslProtocols = SslProtocols.Tls12});
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("dwolla-v2-csharp", ClientVersion));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));
            return client;
        }
    }
}