using System.Net.Http;

namespace Dwolla.Client.Rest
{
    public class RestResponse<T>
    {
        public HttpResponseMessage Response { get; }
        public T Content { get; }
        public string RawContent { get; }
        public RestException Exception { get; }

        public RestResponse(HttpResponseMessage response, T content, RestException exception = null, string rawContent = null)
        {
            Response = response;
            Exception = exception;
            Content = content;
            RawContent = rawContent;
        }
    }
}