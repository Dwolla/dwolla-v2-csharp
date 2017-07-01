using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Dwolla.Client.Tests.Rest
{
    public class FakeClientHandler : HttpClientHandler
    {
        public readonly List<Request> Requests = new List<Request>();
        private readonly Dictionary<Uri, HttpResponseMessage> _responses = new Dictionary<Uri, HttpResponseMessage>();

        public void AddFakeResponse(Uri uri, HttpResponseMessage message) => _responses.Add(uri, message);

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage message, CancellationToken ct)
        {
            var content = message.Content == null ? null : await message.Content.ReadAsStringAsync();
            Requests.Add(new Request(message, content));
            return _responses.ContainsKey(message.RequestUri)
                ? _responses[message.RequestUri]
                : new HttpResponseMessage(HttpStatusCode.NotFound) {RequestMessage = message};
        }
    }

    public class Request
    {
        public HttpRequestMessage Message { get; }
        public string Content { get; }

        public Request(HttpRequestMessage message, string content)
        {
            Message = message;
            Content = content;
        }
    }
}