using System.Net.Http;

namespace Dwolla.Client.Rest
{
    public class RestResponse
    {
        public HttpResponseMessage Response { get; }
        public RestException Exception { get; }

        public RestResponse(HttpResponseMessage resposne, RestException exception = null)
        {
            Response = resposne;
            Exception = exception;
        }
    }

    public class RestResponse<T> : RestResponse
    {
        public T Content { get; }

        public RestResponse(HttpResponseMessage response, T content, RestException exception = null)
            : base(response, exception)
        {
            Content = content;
        }
    }
}