using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Dwolla.Client.Models;
using Dwolla.Client.Models.Requests;
using Dwolla.Client.Models.Responses;
using Dwolla.Client.Rest;

[assembly: InternalsVisibleTo("Dwolla.Client.Tests")]

namespace Dwolla.Client
{
    public interface IDwollaClient
    {
        string ApiBaseAddress { get; }

        Task<RestResponse<TRes>> PostAuthAsync<TRes>(Uri uri, AppTokenRequest content, CancellationToken cancellationToken = default) where TRes : IDwollaResponse;
        Task<RestResponse<TRes>> GetAsync<TRes>(Uri uri, Headers headers, CancellationToken cancellationToken = default) where TRes : IDwollaResponse;
        Task<RestResponse<TRes>> PostAsync<TReq, TRes>(Uri uri, TReq content, Headers headers, CancellationToken cancellationToken = default) where TRes : IDwollaResponse;
        Task<RestResponse<EmptyResponse>> DeleteAsync<TReq>(Uri uri, TReq content, Headers headers, CancellationToken cancellationToken = default);
        Task<RestResponse<EmptyResponse>> UploadAsync(Uri uri, UploadDocumentRequest content, Headers headers, CancellationToken cancellationToken = default);
    }

    public class DwollaClient : IDwollaClient
    {
        public const string ContentType = "application/vnd.dwolla.v1.hal+json";
        public string ApiBaseAddress { get; }

        private static readonly JsonSerializerOptions JsonSettings =
            new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

        private static readonly string ClientVersion = typeof(DwollaClient).GetTypeInfo().Assembly
            .GetCustomAttribute<AssemblyFileVersionAttribute>().Version;
#if NET5_0_OR_GREATER
        private static readonly HttpClient StaticHttpClient = new HttpClient(
                new SocketsHttpHandler 
                { 
                    PooledConnectionLifetime = TimeSpan.FromMinutes(2),
                    SslOptions = new SslClientAuthenticationOptions { EnabledSslProtocols = SslProtocols.Tls12 }
                }
            );
#endif
#if NETSTANDARD2_0
        private static HttpClientHandler _staticClientHandler;
        private static DateTime _staticClientHandlerExpirationDate;
#endif
        
        private readonly IRestClient _client;

        static DwollaClient()
        {
#if NET5_0_OR_GREATER
            SetupHttpClientDefaults(StaticHttpClient);
#endif
        }
        
        public static DwollaClient Create(bool isSandbox) =>
            new DwollaClient(new RestClient(JsonSettings), isSandbox);

        public async Task<RestResponse<TRes>> PostAuthAsync<TRes>(
            Uri uri, AppTokenRequest content, CancellationToken cancellationToken = default) where TRes : IDwollaResponse =>
            await SendAsync<TRes>(new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"client_id", content.Key}, {"client_secret", content.Secret}, {"grant_type", content.GrantType}
                })
            }, 
            cancellationToken);

        public async Task<RestResponse<TRes>> GetAsync<TRes>(
            Uri uri, Headers headers, CancellationToken cancellationToken = default) where TRes : IDwollaResponse =>
            await SendAsync<TRes>(CreateRequest(HttpMethod.Get, uri, headers), cancellationToken);

        public async Task<RestResponse<TRes>> PostAsync<TReq, TRes>(
            Uri uri, TReq content, Headers headers, CancellationToken cancellationToken = default) where TRes : IDwollaResponse =>
            await SendAsync<TRes>(CreatePostRequest(uri, content, headers), cancellationToken);

        public async Task<RestResponse<EmptyResponse>> UploadAsync(
            Uri uri, UploadDocumentRequest content, Headers headers, CancellationToken cancellationToken = default) =>
            await SendAsync<EmptyResponse>(CreateUploadRequest(uri, content, headers), cancellationToken);

        public async Task<RestResponse<EmptyResponse>> DeleteAsync<TReq>(Uri uri, TReq content, Headers headers, CancellationToken cancellationToken = default) =>
            await SendAsync<EmptyResponse>(CreateDeleteRequest(uri, content, headers), cancellationToken);

        private async Task<RestResponse<TRes>> SendAsync<TRes>(HttpRequestMessage request, CancellationToken cancellationToken = default) =>
            await _client.SendAsync<TRes>(request, CreateHttpClient(), cancellationToken);

        private static HttpRequestMessage CreateDeleteRequest<TReq>(
            Uri requestUri, TReq content, Headers headers, string contentType = ContentType) =>
            CreateContentRequest(HttpMethod.Delete, requestUri, headers, content, contentType);

        private static HttpRequestMessage CreatePostRequest<TReq>(
            Uri requestUri, TReq content, Headers headers, string contentType = ContentType) =>
            CreateContentRequest(HttpMethod.Post, requestUri, headers, content, contentType);

        private static HttpRequestMessage CreateContentRequest<TReq>(
            HttpMethod method, Uri requestUri, Headers headers, TReq content, string contentType)
        {
            var r = CreateRequest(method, requestUri, headers);
            r.Content = new StringContent(JsonSerializer.Serialize(content, JsonSettings), Encoding.UTF8, contentType);
            return r;
        }

        private static HttpRequestMessage CreateUploadRequest(Uri requestUri, UploadDocumentRequest content,
            Headers headers)
        {
            var r = CreateRequest(HttpMethod.Post, requestUri, headers);
            r.Content = new MultipartFormDataContent("----------Upload")
            {
                {new StringContent(content.DocumentType), "\"documentType\""},
                GetFileContent(content.Document)
            };
            return r;
        }

        private static StreamContent GetFileContent(File file)
        {
            var fc = new StreamContent(file.Stream);
            fc.Headers.ContentType = MediaTypeHeaderValue.Parse(file.ContentType);
            fc.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "\"file\"",
                FileName = $"\"{file.Filename}\""
            };
            return fc;
        }

        private static HttpRequestMessage CreateRequest(HttpMethod method, Uri requestUri, Headers headers)
        {
            var r = new HttpRequestMessage(method, requestUri.AbsoluteUri);
            r.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));
            foreach (var header in headers) r.Headers.Add(header.Key, header.Value);
            return r;
        }

        internal DwollaClient(IRestClient client, bool isSandbox)
        {
            _client = client;
            ApiBaseAddress = isSandbox ? "https://api-sandbox.dwolla.com" : "https://api.dwolla.com";
        }
        
        private static void SetupHttpClientDefaults(HttpClient client)
        {
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("dwolla-v2-csharp", ClientVersion));
        }

        internal static HttpClient CreateHttpClient()
        {
            // https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient-guidelines#recommended-use
#if NETSTANDARD2_0
            if (_staticClientHandler == null || _staticClientHandlerExpirationDate < DateTime.UtcNow)
            {
                _staticClientHandler = new HttpClientHandler();
                _staticClientHandler.SslProtocols = SslProtocols.Tls12;
                _staticClientHandlerExpirationDate = DateTime.UtcNow + TimeSpan.FromMinutes(2);
            }
            
            var client = new HttpClient(_staticClientHandler);
            SetupHttpClientDefaults(client);
            return client;
#endif
#if NET5_0_OR_GREATER
            return StaticHttpClient;
#endif
        }
    }
}