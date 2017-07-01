using System.Net;
using System.Net.Http;
using System.Text;
using Dwolla.Client.Rest;
using Newtonsoft.Json;
using Xunit;

namespace Dwolla.Client.Tests.Rest
{
    public class ResponseBuilderShould
    {
        private readonly ResponseBuilder _builder;

        private static readonly TestResponse Expected = new TestResponse {Message = "testing"};

        public ResponseBuilderShould() => _builder = new ResponseBuilder();

        [Fact]
        public async void SetExceptionOnNullContent()
        {
            var response = CreateResponse();
            response.Content = null;

            var actual = await _builder.Build<TestResponse>(response);

            Assert.Equal("Response content is null, StatusCode=OK", actual.Exception.Message);
            Assert.Null(actual.Content);
            Assert.Equal(response, actual.Response);
        }

        [Fact]
        public async void SetExceptionOnInvalidJson()
        {
            var response = CreateResponse();
            response.Content = new StringContent("{", Encoding.UTF8);

            var actual = await _builder.Build<TestResponse>(response);

            Assert.Equal("Exception parsing JSON, StatusCode=OK, Content='{'",
                actual.Exception.Message);
            Assert.Null(actual.Content);
            Assert.Equal(response, actual.Response);
        }

        [Fact]
        public async void ReturnResponse()
        {
            var response = CreateResponse();

            var actual = await _builder.Build<TestResponse>(response);

            Assert.Null(actual.Exception);
            Assert.Equal(Expected.Message, actual.Content.Message);
            Assert.Equal(response, actual.Response);
        }

        private static HttpResponseMessage CreateResponse() =>
            new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(Expected), Encoding.UTF8)
            };

        public class TestResponse
        {
            public string Message { get; set; }
        }
    }
}