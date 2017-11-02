using System.Net.Http;
using System.Threading.Tasks;

namespace Dwolla.Client.Rest
{
    public interface IRestClient
    {
        Task<RestResponse<T>> SendAsync<T>(HttpRequestMessage request);
    }

    public class RestClient : IRestClient
    {
        private readonly HttpClient _client;
        private readonly IResponseBuilder _builder;

        public RestClient(HttpClient client) : this(client, new ResponseBuilder())
        {
        }
        
        public async Task<RestResponse<T>> SendAsync<T>(HttpRequestMessage request)
        {
            using (var response = await _client.SendAsync(request))
                return await _builder.Build<T>(response);
        }

        internal RestClient(HttpClient client, IResponseBuilder builder)
        {
            _client = client;
            _builder = builder;
        }
    }
}