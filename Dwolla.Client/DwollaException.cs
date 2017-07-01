using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Dwolla.Client.Rest;

namespace Dwolla.Client
{
    public class DwollaException : Exception
    {
        public HttpResponseMessage Response { get; }
        public string Content { get; }
        public string Resource { get; }
        public string RequestId { get; }

        public DwollaException(HttpResponseMessage response, RestException innerException = null)
            : base("Dwolla API Error", innerException)
        {
            Response = response;
            Resource = $"{response.RequestMessage.Method} {response.RequestMessage.RequestUri}";
            RequestId = GetRequestId(response.Headers);
            Content = innerException?.Content;
        }

        public override string Message => $"{base.Message}, Resource=\"{Resource}\", RequestId=\"{RequestId}\"";

        private static string GetRequestId(HttpHeaders responseHeaders)
        {
            responseHeaders.TryGetValues("x-request-id", out var values);
            return values?.FirstOrDefault();
        }
    }
}