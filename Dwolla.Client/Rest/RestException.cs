using System;
using System.Net;

namespace Dwolla.Client.Rest
{
    public class RestException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public string Content { get; }

        public RestException(string message, Exception innerException, HttpStatusCode statusCode, string content)
            : base(message, innerException)
        {
            StatusCode = statusCode;
            Content = content;
        }

        public override string Message =>
            $"{base.Message}, StatusCode={StatusCode}{(Content == null ? "" : $", Content='{Content}'")}";
    }
}