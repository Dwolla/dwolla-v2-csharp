using Dwolla.Client.Models.Responses;
using Dwolla.Client.Rest;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Dwolla.Client.Tests.Rest
{
    public class ResponseBuilderShould
    {
        private const string RequestId = "myRequestId";

        private readonly ResponseBuilder _builder;

        public ResponseBuilderShould() => _builder = new ResponseBuilder();


        [Fact]
        public async void SetErrorOnInvalidJson()
        {
            const string raw = "{";
            var response = CreateErrorResponse(raw);

            var actual = await _builder.Build<TestResponse>(response);

            Assert.Null(actual.Content);
            Assert.Equal("DeserializationException", actual.Error.Code);
            Assert.Equal("Expected depth to be zero at the end of the JSON payload. There is an open JSON object or array that should be closed. Path: $ | LineNumber: 0 | BytePositionInLine: 1.", actual.Error.Message);
            Assert.Equal(raw, actual.RawContent);
            Assert.Equal(RequestId, actual.RequestId);
            Assert.Equal(response, actual.Response);
        }

        [Fact]
        public async void DeserializeErrorResponse()
        {
            var expected = new ErrorResponse { Code = "MyCode", Message = "MyMessage" };
            var raw = JsonConvert.SerializeObject(expected);
            var response = CreateErrorResponse(raw);

            var actual = await _builder.Build<TestResponse>(response);

            Assert.Null(actual.Content);
            Assert.Equal(expected.Code, actual.Error.Code);
            Assert.Equal(expected.Message, actual.Error.Message);
            Assert.Equal(raw, actual.RawContent);
            Assert.Equal(RequestId, actual.RequestId);
            Assert.Equal(response, actual.Response);
        }

        [Fact]
        public async void DeserializeResponse()
        {
            var expected = new TestResponse { TestProp = "testing" };
            var raw = JsonConvert.SerializeObject(expected);
            var response = CreateResponse(raw);

            var actual = await _builder.Build<TestResponse>(response);

            Assert.Equal(expected.TestProp, actual.Content.TestProp);
            Assert.Null(actual.Error);
            Assert.Equal(raw, actual.RawContent);
            Assert.Equal(RequestId, actual.RequestId);
            Assert.Equal(response, actual.Response);
        }

        private static HttpResponseMessage CreateResponse(string content)
        {
            var r = new HttpResponseMessage
            {
                Content = new StringContent(content, Encoding.UTF8)
            };
            r.StatusCode = System.Net.HttpStatusCode.OK;
            r.Headers.Add("x-request-id", RequestId);
            return r;
        }

        private static HttpResponseMessage CreateErrorResponse(string content)
        {
            var r = new HttpResponseMessage { Content = new StringContent(content, Encoding.UTF8) };
            r.StatusCode = System.Net.HttpStatusCode.BadRequest;
            r.Headers.Add("x-request-id", RequestId);
            return r;
        }

        private class TestResponse
        {
            public string TestProp { get; set; }
        }
    }
}