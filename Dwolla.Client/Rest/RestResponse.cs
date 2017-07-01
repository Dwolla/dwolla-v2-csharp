using System.Net.Http;

namespace Dwolla.Client.Rest
{
    public class RestResponse<T>
    {
        public HttpResponseMessage Response { get; }
        public T Content { get; }
        public RestException Exception { get; }

        public RestResponse(HttpResponseMessage response, T content, RestException exception = null)
        {
            Response = response;
            Content = content;
            Exception = exception;
        }
    }
}