using System;
using System.Net.Http;
using Dwolla.Client.Rest;
using Moq;
using Xunit;

namespace Dwolla.Client.Tests.Rest
{
    public class RestClientShould
    {
        private readonly FakeClientHandler _handler;
        private readonly Mock<IResponseBuilder> _builder;
        private readonly RestClient _restClient;

        private readonly HttpRequestMessage _request;
        private readonly RestResponse<TestResponse> _response;

        public RestClientShould()
        {
            _handler = new FakeClientHandler();
            _builder = new Mock<IResponseBuilder>();
            _restClient = new RestClient(_builder.Object);

            _request = new HttpRequestMessage(HttpMethod.Get, new Uri("https://example.com/foo"));
            _response = new RestResponse<TestResponse>(new HttpResponseMessage(), new TestResponse(), null);
        }

        [Fact]
        public async void ProxySendAsync()
        {
            var response = new HttpResponseMessage();
            _handler.AddFakeResponse(_request.RequestUri, response);
            _builder.Setup(x => x.Build<TestResponse>(response)).ReturnsAsync(_response);

            var actual = await _restClient.SendAsync<TestResponse>(_request);

            Assert.Equal(_response, actual);
            Assert.Single(_handler.Requests);
        }

        [Fact]
        public async void HandleException()
        {
            _builder.Setup(x => x.Error<TestResponse>(null, "HttpClientException", "Not Found", null)).Returns(_response);

            var actual = await _restClient.SendAsync<TestResponse>(_request);

            Assert.Equal(_response, actual);
            Assert.Single(_handler.Requests);
        }

        private class TestResponse
        {
        }
    }
}