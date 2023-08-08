using Dwolla.Client.HttpServices.Architecture;
using Dwolla.Client.Models.Responses;
using Dwolla.Client.Rest;
using System;
using System.Threading.Tasks;

namespace Dwolla.Client.HttpServices
{
    public class RootHttpService : BaseHttpService
    {
        public RootHttpService(IDwollaClient dwollaClient, Func<Task<string>> getAccessTokenAsync)
            : base(dwollaClient, getAccessTokenAsync)
        {
        }

        public async Task<RestResponse<RootResponse>> GetAsync()
        {
            return await GetAsync<RootResponse>(new Uri(client.ApiBaseAddress));
        }
    }
}
