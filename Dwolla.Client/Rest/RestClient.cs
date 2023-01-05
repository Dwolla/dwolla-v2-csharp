using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dwolla.Client.Rest
{
    public interface IRestClient
    {
        Task<RestResponse<T>> SendAsync<T>(HttpRequestMessage request, HttpClient httpClient);
    }

    public class RestClient : IRestClient
    {
        private readonly IResponseBuilder _builder;

        public RestClient() : this(new ResponseBuilder())
        {
        }

        public async Task<RestResponse<T>> SendAsync<T>(HttpRequestMessage request, HttpClient httpClient)
        {
            try
            {
                using (var response = await httpClient.SendAsync(request))
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