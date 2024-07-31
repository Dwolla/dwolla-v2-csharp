using Dwolla.Client.HttpServices.Architecture;
using Dwolla.Client.Models.Requests;
using Dwolla.Client.Models.Responses;
using Dwolla.Client.Rest;
using System;
using System.Collections.Generic;
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

		public async Task<RestResponse<Customer>> GetCustomerAsync(string customerId, CancellationToken cancellation = default)
		{
			if (string.IsNullOrWhiteSpace(customerId))
			{
				throw new ArgumentException("CustomerId should not be blank.");
			}

			return await GetAsync<Customer>(new Uri($"{client.ApiBaseAddress}/customers/{customerId}"), cancellation);
		}

		public async Task<RestResponse<GetCustomersResponse>> GetCustomerCollectionAsync(string search, string email, List<string> status, int? limit, int? offset, CancellationToken cancellation = default)
		{
			var uri = GetWithQueryString($"{client.ApiBaseAddress}/customers", limit, offset, search, email, status);

			return await GetAsync<GetCustomersResponse>(uri, cancellation);
		}

		public async Task<RestResponse<EmptyResponse>> CreateCustomerAsync(CreateCustomerRequest request, string idempotencyKey = null, CancellationToken cancellationToken = default)
		{
			if (request == null) throw new ArgumentNullException(nameof(request));

			return await PostAsync<CreateCustomerRequest, EmptyResponse>(new Uri($"{client.ApiBaseAddress}/customers"), request, idempotencyKey, cancellationToken);
		}

		public async Task<RestResponse<Customer>> UpdateCustomerAsync(string customerId, UpdateCustomerRequest request, string idempotencyKey = null, CancellationToken cancellationToken = default)
		{
			if (string.IsNullOrWhiteSpace(customerId))
			{
				throw new ArgumentException("CustomerId should not be blank.");
			}

			if (request == null) throw new ArgumentNullException(nameof(request));

			return await PostAsync<UpdateCustomerRequest, Customer>(new Uri($"{client.ApiBaseAddress}/customers/{customerId}"), request, idempotencyKey, cancellationToken);
		}
	}
}
