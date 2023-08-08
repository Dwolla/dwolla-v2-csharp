using Dwolla.Client.Models;
using Dwolla.Client.Models.Requests;
using Dwolla.Client.Models.Responses;
using Dwolla.Client.Rest;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Xunit;
using File = Dwolla.Client.Models.File;

namespace Dwolla.Client.Tests
{
    public class DwollaClientShould
    {
        private const string JsonV1 = "application/vnd.dwolla.v1.hal+json";
        private const string RequestId = "some-id";
        private const string UserAgent = "dwolla-v2-csharp/6.0.0";
        private static readonly Uri RequestUri = new Uri("https://api-sandbox.dwolla.com/foo");
        private static readonly Uri AuthRequestUri = new Uri("https://accounts-sandbox.dwolla.com/foo");
        private static readonly Headers Headers = new Headers { { "key1", "value1" }, { "key2", "value2" } };
        private static readonly TestRequest Request = new TestRequest { Message = "requestTest" };
        private static readonly TestResponse Response = new TestResponse { Message = "responseTest" };

        private readonly Mock<IRestClient> _restClient;
        private readonly DwollaClient _client;

        public DwollaClientShould()
        {
            _restClient = new Mock<IRestClient>();
            _client = new DwollaClient(_restClient.Object, true);
        }

        [Fact]
        public void ConfigureHttpClient()
        {
            var client = DwollaClient.CreateHttpClient();
            Assert.Equal(UserAgent, client.DefaultRequestHeaders.UserAgent.ToString());
        }

        [Fact]
        public async void CreatePostAuthRequestAndPassToClient()
        {
            var response = CreateRestResponse(HttpMethod.Post, Response);
            var req = new AppTokenRequest { Key = "key", Secret = "secret" };
            var request = CreateAuthHttpRequest(req);
            _restClient.Setup(x => x.SendAsync<TestResponse>(It.IsAny<HttpRequestMessage>(), It.IsAny<HttpClient>(), default))
                .Callback<HttpRequestMessage, HttpClient>((y, c) => AppTokenCallback(request, y)).ReturnsAsync(response);

            var actual = await _client.PostAuthAsync<TestResponse>(AuthRequestUri, req);

            Assert.Equal(response, actual);
        }

        [Fact]
        public async void CreateGetRequestAndPassToClient()
        {
            var response = CreateRestResponse(HttpMethod.Get, Response);
            SetupForGet(CreateRequest(HttpMethod.Get), response);

            var actual = await _client.GetAsync<TestResponse>(RequestUri, Headers);

            Assert.Equal(response, actual);
        }

        [Fact]
        public async void CreatePostRequestAndPassToClient()
        {
            var response = CreateRestResponse(HttpMethod.Post, Response);
            SetupForPost(CreatePostRequest(), response);

            var actual = await _client.PostAsync<TestRequest, TestResponse>(RequestUri, Request, Headers);

            Assert.Equal(response, actual);
        }

        [Fact]
        public async void CreateUploadRequestAndPassToClient()
        {
            var request = CreateUploadRequest();
            var response = CreateRestResponse<EmptyResponse>(HttpMethod.Post);
            SetupForUpload(CreateUploadRequest(request), response);

            var actual = await _client.UploadAsync(RequestUri, request, Headers);

            Assert.Equal(response, actual);
        }

        [Fact]
        public async void CreateDeleteRequestAndPassToClient()
        {
            var response = CreateRestResponse<EmptyResponse>(HttpMethod.Delete);
            SetupForDelete(CreateDeleteRequest(Request), response);

            var actual = await _client.DeleteAsync(RequestUri, Request, Headers);

            Assert.Equal(response, actual);
        }

        private static HttpRequestMessage CreatePostRequest() => CreateContentRequest(HttpMethod.Post, Request);

        private static UploadDocumentRequest CreateUploadRequest() => new UploadDocumentRequest
        {
            DocumentType = "idCard",
            Document = new File
            {
                ContentType = "image/png",
                Filename = "test.png",
                Stream = new Mock<Stream>().Object
            }
        };

        private static HttpRequestMessage CreateUploadRequest(UploadDocumentRequest request)
        {
            var r = CreateRequest(HttpMethod.Post);
            r.Content = new MultipartFormDataContent("----------Upload")
            {
                {new StringContent(request.DocumentType), "\"documentType\""},
                GetFileContent(request.Document)
            };
            return r;
        }

        private static HttpRequestMessage CreateDeleteRequest(TestRequest content) =>
            CreateContentRequest(HttpMethod.Delete, content);

        private static HttpRequestMessage CreateContentRequest(HttpMethod method, TestRequest content)
        {
            var r = CreateRequest(method);
            r.Content = content != null
                ? new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, JsonV1)
                : null;
            return r;
        }

        private static HttpRequestMessage CreateRequest(HttpMethod method)
        {
            var r = new HttpRequestMessage(method, RequestUri);
            r.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(JsonV1));
            foreach (var (key, value) in Headers) r.Headers.Add(key, value);
            return r;
        }

        private static RestResponse<T> CreateRestResponse<T>(
            HttpMethod method, T content = null, string rawContent = null) where T : class
        {
            var r = new HttpResponseMessage
            {
                RequestMessage = new HttpRequestMessage { RequestUri = RequestUri, Method = method }
            };
            r.Headers.Add("x-request-id", RequestId);
            return new RestResponse<T>(r, content, rawContent);
        }

        private static HttpRequestMessage CreateAuthHttpRequest(AppTokenRequest req) =>
            new HttpRequestMessage(HttpMethod.Post, AuthRequestUri)
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"client_id", req.Key}, {"client_secret", req.Secret}, {"grant_type", req.GrantType}
                })
            };

        private void SetupForGet(HttpRequestMessage req, RestResponse<TestResponse> res) =>
            _restClient.Setup(x => x.SendAsync<TestResponse>(It.IsAny<HttpRequestMessage>(), It.IsAny<HttpClient>(), default))
                .Callback<HttpRequestMessage, HttpClient>((y, c) => GetCallback(req, y)).ReturnsAsync(res);

        private void SetupForPost<T>(HttpRequestMessage req, RestResponse<T> res) =>
            _restClient.Setup(x => x.SendAsync<T>(It.IsAny<HttpRequestMessage>(), It.IsAny<HttpClient>(), default))
                .Callback<HttpRequestMessage, HttpClient>((y, c) => PostCallback(req, y)).ReturnsAsync(res);

        private void SetupForUpload(HttpRequestMessage r, RestResponse<EmptyResponse> response) =>
            _restClient.Setup(x => x.SendAsync<EmptyResponse>(It.IsAny<HttpRequestMessage>(), It.IsAny<HttpClient>(), default))
                .Callback<HttpRequestMessage, HttpClient>((y, c) => UploadCallback(r, y)).ReturnsAsync(response);

        private void SetupForDelete(HttpRequestMessage req, RestResponse<EmptyResponse> res) =>
            _restClient.Setup(x => x.SendAsync<EmptyResponse>(It.IsAny<HttpRequestMessage>(), It.IsAny<HttpClient>(), default))
                .Callback<HttpRequestMessage, HttpClient>((y, c) => DeleteCallback(req, y)).ReturnsAsync(res);

        private static async void PostCallback(HttpRequestMessage expected, HttpRequestMessage actual)
        {
            GetCallback(expected, actual);
            Assert.Equal("{\"message\":\"requestTest\"}", await actual.Content.ReadAsStringAsync());
            Assert.Equal("application/vnd.dwolla.v1.hal+json; charset=utf-8",
                actual.Content.Headers.ContentType.ToString());
        }

        private static async void UploadCallback(HttpRequestMessage expected, HttpRequestMessage actual)
        {
            GetCallback(expected, actual);
            var content = await actual.Content.ReadAsStringAsync();
            Assert.Contains("----------Upload", content);
            Assert.Contains("documentType", content);
            Assert.Contains("file", content);
            Assert.Equal("multipart/form-data; boundary=\"----------Upload\"",
                actual.Content.Headers.ContentType.ToString());
        }

        private static void DeleteCallback(HttpRequestMessage expected, HttpRequestMessage actual) =>
            GetCallback(expected, actual);

        private static void GetCallback(HttpRequestMessage expected, HttpRequestMessage actual)
        {
            Assert.Equal(expected.Method, actual.Method);
            Assert.Equal(expected.RequestUri, actual.RequestUri);
            foreach (var key in Headers.Keys) Assert.True(AssertHeader(expected, actual, key));
        }

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private static async void AppTokenCallback(HttpRequestMessage expected, HttpRequestMessage actual)
        {
            Assert.Equal(expected.Method, actual.Method);
            Assert.Equal(expected.RequestUri, actual.RequestUri);
            Assert.Equal("client_id=key&client_secret=secret&grant_type=client_credentials", await actual.Content.ReadAsStringAsync());
            Assert.Equal("application/x-www-form-urlencoded", actual.Content.Headers.ContentType.ToString());
        }

        private static bool AssertHeader(HttpRequestMessage expected, HttpRequestMessage actual, string key) =>
            expected.Headers.GetValues(key).ToString() == actual.Headers.GetValues(key).ToString();

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

        private class TestRequest
        {
            // ReSharper disable once UnusedAutoPropertyAccessor.Local
            public string Message { get; set; }
        }

        private class TestResponse : IDwollaResponse
        {
            // ReSharper disable once UnusedAutoPropertyAccessor.Local
            public string Message { get; set; }
        }
    }
}
