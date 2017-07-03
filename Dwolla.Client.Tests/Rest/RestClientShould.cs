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

        public RestClientShould()
        {
            _handler = new FakeClientHandler();
            _builder = new Mock<IResponseBuilder>();
            _restClient = new RestClient(new HttpClient(_handler), _builder.Object);
        }

        [Fact]
        public async void ProxySendAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri("https://example.com/foo"));
            var response = new HttpResponseMessage();
            _handler.AddFakeResponse(request.RequestUri, response);
            var expected = new RestResponse<TestResponse>(new HttpResponseMessage(), new TestResponse());
            _builder.Setup(x => x.Build<TestResponse>(response)).ReturnsAsync(expected);

            var actual = await _restClient.SendAsync<TestResponse>(request);

            Assert.Equal(expected, actual);
            Assert.Equal(1, _handler.Requests.Count);
        }

        private class TestResponse
        {
        }
    }
}