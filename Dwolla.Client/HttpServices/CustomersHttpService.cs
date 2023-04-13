using Dwolla.Client.HttpServices.Architecture;
using Dwolla.Client.Models.Requests;
using Dwolla.Client.Models.Responses;
using Dwolla.Client.Rest;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dwolla.Client.HttpServices
{
	public class CustomersHttpService : BaseHttpService
	{
		public CustomersHttpService(IDwollaClient dwollaClient, Func<Task<string>> getAccessToken)
			: base(dwollaClient, getAccessToken)
		{
		}

		public async Task<RestResponse<GetCustomersResponse>> GetCollectionAsync(Uri uri, CancellationToken cancellation = default)
		{
			if (uri == null) throw new ArgumentNullException(nameof(uri));

			return await GetAsync<GetCustomersResponse>(uri, cancellation);
		}

		public async Task<RestResponse<Customer>> GetAsync(Uri uri, CancellationToken cancellation = default)
		{
			if (uri == null) throw new ArgumentNullException(nameof(uri));

			return await GetAsync<Customer>(uri, cancellation);
		}

		public async Task<RestResponse<EmptyResponse>> CreateAsync(Uri uri, CreateCustomerRequest request, string idempotencyKey = null, CancellationToken cancellationToken = default)
		{
			if (uri == null) throw new ArgumentNullException(nameof(uri));

			if (request == null) throw new ArgumentNullException(nameof(request));

			return await PostAsync<CreateCustomerRequest, EmptyResponse>(uri, request, idempotencyKey, cancellationToken);
		}
	}
}
