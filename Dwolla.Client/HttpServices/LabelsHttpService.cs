using Dwolla.Client.HttpServices.Architecture;
using Dwolla.Client.Models.Requests;
using Dwolla.Client.Models.Responses;
using Dwolla.Client.Rest;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dwolla.Client.HttpServices
{
    public class LabelsHttpService : BaseHttpService
    {
        public LabelsHttpService(IDwollaClient dwollaClient, Func<Task<string>> getAccessToken)
           : base(dwollaClient, getAccessToken)
        {
        }

        public async Task<RestResponse<Label>> GetLabelAsync(string labelId, CancellationToken cancellation = default)
        {
            if (string.IsNullOrWhiteSpace(labelId))
            {
                throw new ArgumentException("LabelId should not be blank.");
            }

            return await GetAsync<Label>(new Uri($"{client.ApiBaseAddress}/labels/{labelId}"), cancellation);
        }

        public async Task<RestResponse<GetLabelsResponse>> GetLabelCustomerCollectionAsync(string customerId, int? limit, int? offset, CancellationToken cancellation = default)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("CustomerId should not be blank.");
            }

            var url = $"{client.ApiBaseAddress}/customers/{customerId}/labels";
            var qb = new QueryBuilder();

            if (limit.HasValue)
            {
                qb.Add("limit", limit.ToString());
            }

            if (offset.HasValue)
            {
                qb.Add("offset", offset.ToString());
            }

            return await GetAsync<GetLabelsResponse>(new Uri(url + qb.ToQueryString()), cancellation);
        }

        public async Task<RestResponse<LabelLedgerEntry>> GetLedgerEntryAsync(string labelId, CancellationToken cancellation = default)
        {
            if (string.IsNullOrWhiteSpace(labelId))
            {
                throw new ArgumentException("LabelId should not be blank.");
            }

            return await GetAsync<LabelLedgerEntry>(new Uri($"{client.ApiBaseAddress}/ledger-entries/{labelId}"), cancellation);
        }

        public async Task<RestResponse<GetLabelLedgerEntriesResponse>> GetLedgerEntryCollectionAsync(string labelId, int? limit, int? offset, CancellationToken cancellation = default)
        {
            if (string.IsNullOrWhiteSpace(labelId))
            {
                throw new ArgumentException("LabelId should not be blank.");
            }

            var url = $"{client.ApiBaseAddress}labels/{labelId}/ledger-entries";
            var qb = new QueryBuilder();

            if (limit.HasValue)
            {
                qb.Add("limit", limit.ToString());
            }

            if (offset.HasValue)
            {
                qb.Add("offset", offset.ToString());
            }

            return await GetAsync<GetLabelLedgerEntriesResponse>(new Uri(url + qb.ToQueryString()), cancellation);
        }

        public async Task<RestResponse<LabelReallocation>> GetReallocationAsync(string labelReallocationId, CancellationToken cancellation = default)
        {
            if (string.IsNullOrWhiteSpace(labelReallocationId))
            {
                throw new ArgumentException("LabelReallocationId should not be blank.");
            }

            return await GetAsync<LabelReallocation>(new Uri($"{client.ApiBaseAddress}/label-reallocations/{labelReallocationId}"), cancellation);
        }

        public async Task<RestResponse<EmptyResponse>> CreateLabelAsync(string labelId, CreateLabelRequest request, string idempotencyKey = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(labelId))
            {
                throw new ArgumentException("LabelId should not be blank.");
            }

            if (request == null) throw new ArgumentNullException(nameof(request));

            return await PostAsync<CreateLabelRequest, EmptyResponse>(new Uri($"{client.ApiBaseAddress}/customers/{labelId}/labels"), request, idempotencyKey, cancellationToken);
        }

        public async Task<RestResponse<EmptyResponse>> CreateLabelReallocationAsync(CreateLabelReallocationRequest request, string idempotencyKey = null, CancellationToken cancellationToken = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            return await PostAsync<CreateLabelReallocationRequest, EmptyResponse>(new Uri($"{client.ApiBaseAddress}/label-reallocations"), request, idempotencyKey, cancellationToken);
        }

        public async Task<RestResponse<EmptyResponse>> CreateLedgerEntryAsync(string labelId, CreateLabelLedgerEntryRequest request, string idempotencyKey = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(labelId))
            {
                throw new ArgumentException("LabelId should not be blank.");
            }

            if (request == null) throw new ArgumentNullException(nameof(request));

            return await PostAsync<CreateLabelLedgerEntryRequest, EmptyResponse>(new Uri($"{client.ApiBaseAddress}/labels/{labelId}/ledger-entries"), request, idempotencyKey, cancellationToken);
        }

        public async Task<RestResponse<EmptyResponse>> DeleteLabelAsync(string labelId)
        {
            if (string.IsNullOrWhiteSpace(labelId))
            {
                throw new ArgumentException("LabelId should not be blank.");
            }

            return await DeleteAsync<EmptyResponse>(new Uri($"{client.ApiBaseAddress}/labels/{labelId}"), null);
        }
    }
}
