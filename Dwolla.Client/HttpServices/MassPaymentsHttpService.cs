using Dwolla.Client.HttpServices.Architecture;
using Dwolla.Client.Models.Requests;
using Dwolla.Client.Models.Responses;
using Dwolla.Client.Rest;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dwolla.Client.HttpServices
{
    public class MassPaymentsHttpService : BaseHttpService
    {
        public MassPaymentsHttpService(IDwollaClient dwollaClient, Func<Task<string>> getAccessToken)
            : base(dwollaClient, getAccessToken)
        {
        }

        public async Task<RestResponse<MasspaymentResponse>> GetMassPaymentAsync(string massPaymentId, CancellationToken cancellation = default)
        {
            if (string.IsNullOrWhiteSpace(massPaymentId))
            {
                throw new ArgumentException("MassPaymentId should not be blank.");
            }

            return await GetAsync<MasspaymentResponse>(new Uri($"{client.ApiBaseAddress}/mass-payments/{massPaymentId}"), cancellation);
        }

        public async Task<RestResponse<EmptyResponse>> CreateMassPaymentAsync(CreateMasspaymentRequest request, string idempotencyKey = null, CancellationToken cancellationToken = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            return await PostAsync<CreateMasspaymentRequest, EmptyResponse>(new Uri($"{client.ApiBaseAddress}/mass-payments"), request, idempotencyKey, cancellationToken);
        }
    }
}
