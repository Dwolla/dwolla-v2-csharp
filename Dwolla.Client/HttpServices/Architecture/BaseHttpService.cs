using Dwolla.Client.Models;
using Dwolla.Client.Models.Requests;
using Dwolla.Client.Models.Responses;
using Dwolla.Client.Rest;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dwolla.Client.HttpServices.Architecture
{
	public abstract class BaseHttpService
	{
		internal Func<Task<string>> getAccessToken;
		internal IDwollaClient client;

		public BaseHttpService(IDwollaClient dwollaClient, Func<Task<string>> getAccessToken)
		{
			client = dwollaClient;
			this.getAccessToken = getAccessToken;
		}

		internal async Task<RestResponse<TRes>> GetAsync<TRes>(Uri uri, CancellationToken cancellationToken = default) where TRes : IDwollaResponse
		{
			var headers = await CreateHeaders();

			return await ExecAsync(() => client.GetAsync<TRes>(uri, headers, cancellationToken));
		}

		internal async Task<RestResponse<EmptyResponse>> PostAsync<TReq>(Uri uri, TReq request, CancellationToken cancellationToken = default)
		{
			var headers = await CreateHeaders();

			return await ExecAsync(() => client.PostAsync<TReq, EmptyResponse>(uri, request, headers, cancellationToken));
		}

		internal async Task<RestResponse<TRes>> PostAsync<TReq, TRes>(Uri uri, TReq request, string idempotencyKey, CancellationToken cancellationToken = default) where TRes : IDwollaResponse
		{
			var headers = await CreateHeaders(idempotencyKey);

			return await ExecAsync(() => client.PostAsync<TReq, TRes>(uri, request, headers, cancellationToken));
		}

		internal async Task<RestResponse<EmptyResponse>> DeleteAsync<TReq>(Uri uri, TReq request)
		{
			var headers = await CreateHeaders();

			return await ExecAsync(() => client.DeleteAsync(uri, request, headers));
		}
        internal async Task<RestResponse<EmptyResponse>> UploadAsync(Uri uri, UploadDocumentRequest request)
        {
            var headers = await CreateHeaders();

			return await ExecAsync(() => client.UploadAsync(uri, request, headers));
        }

        private async Task<Headers> CreateHeaders(string idempotencyKey = null)
		{
			var headers = new Headers
			{
				["Authorization"] = $"Bearer {await getAccessToken()}"
			};

			if (idempotencyKey != null)
			{
				headers["Idempotency-Key"] = idempotencyKey;
			}

			return headers;
		}

		private async Task<RestResponse<TRes>> ExecAsync<TRes>(Func<Task<RestResponse<TRes>>> func) where TRes : IDwollaResponse
		{
			var r = await func();

			if (r?.Error?.Code == "ExpiredAccessToken")
			{
				await getAccessToken();

				r = await func();
			}

			return r;
		}
	}
}
