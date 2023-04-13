using Dwolla.Client.HttpServices.Architecture;
using Dwolla.Client.Models;
using Dwolla.Client.Models.Requests;
using Dwolla.Client.Models.Responses;
using Dwolla.Client.Rest;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dwolla.Client.HttpRequestServices
{
	public class MicroDepositsHttpService : BaseHttpService
	{
		public MicroDepositsHttpService(IDwollaClient dwollaClient,	Func<Task<string>> getAccessTokenAsync)
			: base(dwollaClient, getAccessTokenAsync)
		{
		}

		public async Task<RestResponse<MicroDepositsResponse>> GetAsync(string fundingSourceId)
		{
			if (string.IsNullOrWhiteSpace(fundingSourceId))
			{
				throw new ArgumentException("FundingSourceId should not be null or whitespace.");
			}

			return await GetAsync<MicroDepositsResponse>(new Uri($"{client.ApiBaseAddress}/funding-sources/{fundingSourceId}/micro-deposits"));
		}

		public async Task<RestResponse<EmptyResponse>> VerifyAsync(string fundingSourceId, decimal amount1, decimal amount2, string currency = "USD", CancellationToken cancellationToken = default)
		{
			if (string.IsNullOrWhiteSpace(fundingSourceId))
			{
				throw new ArgumentException("FundingSourceId should not be null or whitespace.");
			}

			if (string.IsNullOrWhiteSpace(currency))
			{
				throw new ArgumentException("currency should not be null or whitespace.");
			}

			return await PostAsync(new Uri($"{client.ApiBaseAddress}/funding-sources/{fundingSourceId}/micro-deposits"),
				new MicroDepositsRequest
				{
					Amount1 = new Money { Value = amount1, Currency = currency },
					Amount2 = new Money { Value = amount2, Currency = currency }
				},
				cancellationToken);
		}
	}
}
