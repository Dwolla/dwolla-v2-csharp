using Dwolla.Client.HttpServices.Architecture;
using Dwolla.Client.Models.Requests;
using Dwolla.Client.Models.Responses;
using Dwolla.Client.Rest;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dwolla.Client.HttpServices
{
    public class BeneficialOwnersHttpService : BaseHttpService
    {
        public BeneficialOwnersHttpService(IDwollaClient dwollaClient, Func<Task<string>> getAccessToken)
            : base(dwollaClient, getAccessToken)
        {
        }

        public async Task<RestResponse<BeneficialOwnerResponse>> GetBeneficialOwnerAsync(string beneficialOwnerId, CancellationToken cancellation = default)
        {
            if (string.IsNullOrWhiteSpace(beneficialOwnerId))
            {
                throw new ArgumentException("BeneficialOwnerId should not be blank.");
            }

            return await GetAsync<BeneficialOwnerResponse>(new Uri($"{client.ApiBaseAddress}/beneficial-owners/{beneficialOwnerId}"), cancellation);
        }

        public async Task<RestResponse<GetBeneficialOwnersResponse>> GetBeneficialOwnerCollectionAsync(string customerId, CancellationToken cancellation = default)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("CustomerId should not be blank.");
            }

            return await GetAsync<GetBeneficialOwnersResponse>(new Uri($"{client.ApiBaseAddress}/customers/{customerId}/beneficial-owners"), cancellation);
        }

        public async Task<RestResponse<BeneficialOwnershipResponse>> GetBeneficialOwnershipAsync(string customerId, CancellationToken cancellation = default)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("CustomerId should not be blank.");
            }

            return await GetAsync<BeneficialOwnershipResponse>(new Uri($"{client.ApiBaseAddress}/customers/{customerId}/beneficial-ownership"), cancellation);
        }

        public async Task<RestResponse<EmptyResponse>> CreateBeneficialOwnerAsync(string customerId, CreateBeneficialOwnerRequest request, string idempotencyKey = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("CustomerId should not be blank.");
            }

            return await PostAsync<CreateBeneficialOwnerRequest, EmptyResponse>(new Uri($"{client.ApiBaseAddress}/customers/{customerId}/beneficial-owners"), request, idempotencyKey, cancellationToken);
        }

        public async Task<RestResponse<EmptyResponse>> CertifyBeneficialOwnerAsync(string customerId, CertifyBeneficialOwnershipRequest request, string idempotencyKey = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("CustomerId should not be blank.");
            }

            if (request == null) throw new ArgumentNullException(nameof(request));

            return await PostAsync<CertifyBeneficialOwnershipRequest, EmptyResponse>(new Uri($"{client.ApiBaseAddress}/customers/{customerId}/beneficial-ownership"), request, idempotencyKey, cancellationToken);
        }

        public async Task<RestResponse<EmptyResponse>> DeleteBeneficialOwnerAsync(string beneficialOwnerId)
        {
            if (string.IsNullOrWhiteSpace(beneficialOwnerId))
            {
                throw new ArgumentException("BeneficialOwnerId should not be blank.");
            }

            return await DeleteAsync<EmptyResponse>(new Uri($"{client.ApiBaseAddress}/beneficial-owners/{beneficialOwnerId}"), null);
        }
    }
}
