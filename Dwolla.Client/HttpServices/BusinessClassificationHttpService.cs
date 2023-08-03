using Dwolla.Client.HttpServices.Architecture;
using Dwolla.Client.Models.Responses;
using Dwolla.Client.Rest;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dwolla.Client.HttpServices
{
    public class BusinessClassificationHttpService : BaseHttpService
    {
        public BusinessClassificationHttpService(IDwollaClient dwollaClient, Func<Task<string>> getAccessToken)
           : base(dwollaClient, getAccessToken)
        {
        }

        public async Task<RestResponse<BusinessClassification>> GetBusinessClassificationAsync(string businessClassificationId, CancellationToken cancellation = default)
        {
            if (string.IsNullOrWhiteSpace(businessClassificationId))
            {
                throw new ArgumentException("BusinessClassificationId should not be blank.");
            }

            return await GetAsync<BusinessClassification>(new Uri($"{client.ApiBaseAddress}/business-classifications/{businessClassificationId}"), cancellation);
        }

        public async Task<RestResponse<GetBusinessClassificationsResponse>> GetBusinessClassificationCollectionAsync(CancellationToken cancellation = default)
        {
            return await GetAsync<GetBusinessClassificationsResponse>(new Uri($"{client.ApiBaseAddress}/business-classifications"), cancellation);
        }
    }
}
