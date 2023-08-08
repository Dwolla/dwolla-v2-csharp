using Dwolla.Client.HttpServices.Architecture;
using Dwolla.Client.Models.Requests;
using Dwolla.Client.Models.Responses;
using Dwolla.Client.Rest;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dwolla.Client.HttpServices
{
    public class DocumentsHttpService : BaseHttpService
    {
        public DocumentsHttpService(IDwollaClient dwollaClient, Func<Task<string>> getAccessToken)
           : base(dwollaClient, getAccessToken)
        {
        }

        public async Task<RestResponse<DocumentResponse>> GetDocumentAsync(string documentId, CancellationToken cancellation = default)
        {
            if (string.IsNullOrWhiteSpace(documentId))
            {
                throw new ArgumentException("DocumentId should not be blank.");
            }

            return await GetAsync<DocumentResponse>(new Uri($"{client.ApiBaseAddress}/documents/{documentId}"), cancellation);
        }

        public async Task<RestResponse<GetDocumentsResponse>> GetDocumentCollectionAsync(string customerId, CancellationToken cancellation = default)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("CustomerId should not be blank.");
            }

            return await GetAsync<GetDocumentsResponse>(new Uri($"{client.ApiBaseAddress}/customers/{customerId}"), cancellation);
        }

        public async Task<RestResponse<EmptyResponse>> UploadDocumentAsync(string customerId, UploadDocumentRequest request)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("CustomerId should not be blank.");
            }

            if (request == null) throw new ArgumentNullException(nameof(request));

            return await UploadAsync(new Uri($"{client.ApiBaseAddress}/customers/{customerId}/documents"), request);
        }
    }
}
