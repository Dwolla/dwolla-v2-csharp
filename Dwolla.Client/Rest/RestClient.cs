using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace Dwolla.Client.Rest
{
    public interface IRestClient
    {
        Task<RestResponse<T>> SendAsync<T>(HttpRequestMessage request);
    }

    public class RestClient : IRestClient
    {
        private static readonly string ClientVersion = typeof(RestClient).GetTypeInfo().Assembly
            .GetCustomAttribute<AssemblyFileVersionAttribute>().Version;

        private static readonly HttpClient _client = new HttpClient(new HttpClientHandler {SslProtocols = SslProtocols.Tls12, });
        private readonly IResponseBuilder _builder;

        public RestClient() : this(new ResponseBuilder())
        {
        }

        public async Task<RestResponse<T>> SendAsync<T>(HttpRequestMessage request)
        {

            try
            {
                using (var response = await _client.SendAsync(request))
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
            _client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("dwolla-v2-csharp", ClientVersion));

            _builder = builder;
        }
    }
}