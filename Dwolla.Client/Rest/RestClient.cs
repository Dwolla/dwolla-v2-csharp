using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Dwolla.Client.Rest
{
    public interface IRestClient
    {
        Task<RestResponse<T>> SendAsync<T>(HttpRequestMessage request, HttpClient httpClient, CancellationToken cancellation = default);
    }

    public class RestClient : IRestClient
    {
        private readonly IResponseBuilder _builder;

        public RestClient(JsonSerializerOptions jsonSerializerOptions) : this(new ResponseBuilder(jsonSerializerOptions))
        {
        }

        public async Task<RestResponse<T>> SendAsync<T>(HttpRequestMessage request, HttpClient httpClient, CancellationToken cancellationToken = default)
        {
            try
            {
                using (var response = await httpClient.SendAsync(request, cancellationToken))
                {
                    return await _builder.Build<T>(response);
                }
            }
            catch (Exception e)
            {
                return _builder.Error<T>(null, "HttpClientException", e.Message);
            }
        }

        internal RestClient(IResponseBuilder builder)
        {
            _builder = builder;
        }
    }
}