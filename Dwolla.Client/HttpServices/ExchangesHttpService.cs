using Dwolla.Client.HttpServices.Architecture;
using Dwolla.Client.Models.Requests;
using Dwolla.Client.Models.Responses;
using Dwolla.Client.Rest;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dwolla.Client.HttpServices
{
    public class ExchangesHttpService : BaseHttpService
    {
        public ExchangesHttpService(IDwollaClient dwollaClient, Func<Task<string>> getAccessToken)
            : base(dwollaClient, getAccessToken)
        {
        }

        public async Task<RestResponse<ExchangeResponse>> GetExchangeAsync(string exchangeId, CancellationToken cancellation = default)
        {
            if (string.IsNullOrWhiteSpace(exchangeId))
            {
                throw new ArgumentException("ExchangeId should not be blank.");
            }

            return await GetAsync<ExchangeResponse>(new Uri($"{client.ApiBaseAddress}/exchanges/{exchangeId}"), cancellation);
        }

        public async Task<RestResponse<GetExchangesResponse>> GetExchangeCollectionAsync(CancellationToken cancellation = default)
        {
            return await GetAsync<GetExchangesResponse>(new Uri($"{client.ApiBaseAddress}/exchanges"), cancellation);
        }

        public async Task<RestResponse<GetExchangesResponse>> GetCustomerExchangeCollectionAsync(string customerId, CancellationToken cancellation = default)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("CustomerId should not be blank.");
            }

            return await GetAsync<GetExchangesResponse>(new Uri($"{client.ApiBaseAddress}/customers/{customerId}/exchanges"), cancellation);
        }

        public async Task<RestResponse<ExchangePartnerResponse>> GetPartnerAsync(string exchangePartnerId, CancellationToken cancellation = default)
        {
            if (string.IsNullOrWhiteSpace(exchangePartnerId))
            {
                throw new ArgumentException("ExchangePartnerId should not be blank.");
            }

            return await GetAsync<ExchangePartnerResponse>(new Uri($"{client.ApiBaseAddress}/exchange-partners/{exchangePartnerId}"), cancellation);
        }

        public async Task<RestResponse<GetExchangePartnersResponse>> GetPartnerCollectionAsync(CancellationToken cancellation = default)
        {
            return await GetAsync<GetExchangePartnersResponse>(new Uri($"{client.ApiBaseAddress}/exchange-partners"), cancellation);
        }

        public async Task<RestResponse<EmptyResponse>> CreateExchangeAsync(CreateExchangeRequest request, string idempotencyKey = null, CancellationToken cancellationToken = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            return await PostAsync<CreateExchangeRequest, EmptyResponse>(new Uri($"{client.ApiBaseAddress}/exchanges"), request, idempotencyKey, cancellationToken);
        }

        public async Task<RestResponse<EmptyResponse>> CreateExchangeCustomerAsync(string exchangeId, CreateExchangeRequest request, string idempotencyKey = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(exchangeId))
            {
                throw new ArgumentException("ExchangePartnerId should not be blank.");
            }

            if (request == null) throw new ArgumentNullException(nameof(request));

            return await PostAsync<CreateExchangeRequest, EmptyResponse>(new Uri($"{client.ApiBaseAddress}/customers/{exchangeId}/exchanges"), request, idempotencyKey, cancellationToken);
        }
    }
}
