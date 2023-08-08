using Dwolla.Client.HttpServices.Architecture;
using Dwolla.Client.Models.Requests;
using Dwolla.Client.Models.Responses;
using Dwolla.Client.Rest;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dwolla.Client.HttpServices
{
    public class TransfersHttpService : BaseHttpService
    {
        public TransfersHttpService(IDwollaClient dwollaClient, Func<Task<string>> getAccessToken)
           : base(dwollaClient, getAccessToken)
        {
        }

        public async Task<RestResponse<TransferResponse>> GetTransferAsync(string transferId, CancellationToken cancellation = default)
        {
            if (string.IsNullOrWhiteSpace(transferId))
            {
                throw new ArgumentException("TransferId should not be blank.");
            }

            return await GetAsync<TransferResponse>(new Uri($"{client.ApiBaseAddress}/transfers/{transferId}"), cancellation);
        }

        public async Task<RestResponse<TransferFailureResponse>> GetFailureAsync(string transferId, CancellationToken cancellation = default)
        {
            if (string.IsNullOrWhiteSpace(transferId))
            {
                throw new ArgumentException("TransferId should not be blank.");
            }

            return await GetAsync<TransferFailureResponse>(new Uri($"{client.ApiBaseAddress}/transfers/{transferId}/failure"), cancellation);
        }

        public async Task<RestResponse<EmptyResponse>> CreateTransferAsync(CreateTransferRequest request, string idempotencyKey = null, CancellationToken cancellationToken = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            return await PostAsync<CreateTransferRequest, EmptyResponse>(new Uri($"{client.ApiBaseAddress}/transfers"), request, idempotencyKey, cancellationToken);
        }
    }
}
