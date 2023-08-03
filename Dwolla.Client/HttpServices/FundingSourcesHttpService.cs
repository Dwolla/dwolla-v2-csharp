using Dwolla.Client.HttpServices.Architecture;
using Dwolla.Client.Models.Requests;
using Dwolla.Client.Models.Responses;
using Dwolla.Client.Rest;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dwolla.Client.HttpServices
{
    public class FundingSourcesHttpService : BaseHttpService
    {
        public FundingSourcesHttpService(IDwollaClient dwollaClient, Func<Task<string>> getAccessToken)
           : base(dwollaClient, getAccessToken)
        {
        }

        public async Task<RestResponse<FundingSource>> GetFundingSourceAsync(string fundingSourceId, CancellationToken cancellation = default)
        {
            if (string.IsNullOrWhiteSpace(fundingSourceId))
            {
                throw new ArgumentException("FundingSourceId should not be blank.");
            }

            return await GetAsync<FundingSource>(new Uri($"{client.ApiBaseAddress}/funding-sources/{fundingSourceId}"), cancellation);
        }

        public async Task<RestResponse<GetFundingSourcesResponse>> GetFundingSourceCollectionAsync(string fundingSourceId, CancellationToken cancellation = default)
        {
            if (string.IsNullOrWhiteSpace(fundingSourceId))
            {
                throw new ArgumentException("FundingSourceId should not be blank.");
            }

            return await GetAsync<GetFundingSourcesResponse>(new Uri($"{client.ApiBaseAddress}/funding-sources/{fundingSourceId}/funding-sources"), cancellation);
        }

        public async Task<RestResponse<MicroDepositsResponse>> GetMicroDepositAsync(string fundingSourceId, CancellationToken cancellation = default)
        {
            if (string.IsNullOrWhiteSpace(fundingSourceId))
            {
                throw new ArgumentException("FundingSourceId should not be blank.");
            }

            return await GetAsync<MicroDepositsResponse>(new Uri($"{client.ApiBaseAddress}/funding-sources/{fundingSourceId}/micro-deposits"), cancellation);
        }

        public async Task<RestResponse<EmptyResponse>> VerifyMicroDepositAsync(string fundingSourceId, MicroDepositsRequest request ,CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(fundingSourceId))
            {
                throw new ArgumentException("FundingSourceId should not be blank.");
            }

            if (request == null) throw new ArgumentNullException(nameof(request));

            return await PostAsync(new Uri($"{client.ApiBaseAddress}/funding-sources/{fundingSourceId}/micro-deposits"), request, cancellationToken);
        }

        public async Task<RestResponse<EmptyResponse>> CreateFundingSourceAsync(string customerId, CreateFundingSourceRequest request, string idempotencyKey = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("CustomerId should not be blank.");
            }

            if (request == null) throw new ArgumentNullException(nameof(request));

            return await PostAsync<CreateFundingSourceRequest, EmptyResponse>(new Uri($"{client.ApiBaseAddress}/customers/{customerId}/funding-sources"), request, idempotencyKey, cancellationToken);
        }

        public async Task<RestResponse<EmptyResponse>> CreateFundingSourcePlaidAsync(string customerId, CreatePlaidFundingSourceRequest request, string idempotencyKey = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("CustomerId should not be blank.");
            }

            if (request == null) throw new ArgumentNullException(nameof(request));

            return await PostAsync<CreatePlaidFundingSourceRequest, EmptyResponse>(new Uri($"{client.ApiBaseAddress}/customers/{customerId}/funding-sources"), request, idempotencyKey, cancellationToken);
        }
    }
}
