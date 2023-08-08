using Dwolla.Client.HttpServices.Architecture;
using Dwolla.Client.Models.Requests;
using Dwolla.Client.Models.Responses;
using Dwolla.Client.Rest;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dwolla.Client.HttpServices
{
    public class AuthorizationHttpService : BaseHttpService
    {
        public AuthorizationHttpService(IDwollaClient dwollaClient, Func<Task<string>> getAccessToken)
            : base(dwollaClient, getAccessToken)
        {
        }

        public async Task<RestResponse<TokenResponse>> GetToken(CancellationToken cancellationToken = default)
        {
            return await client.PostAuthAsync<TokenResponse>(
                new Uri($"{client.ApiBaseAddress}/token"), new AppTokenRequest { Key = DwollaConfiguration.Key, Secret = DwollaConfiguration.Secret }, cancellationToken);
        }
    }
}