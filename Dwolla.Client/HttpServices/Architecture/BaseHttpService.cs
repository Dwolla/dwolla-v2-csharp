using Dwolla.Client.Models;
using Dwolla.Client.Models.Requests;
using Dwolla.Client.Models.Responses;
using Dwolla.Client.Rest;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
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
		
		internal async Task<RestResponse<EmptyResponse>> PostAsync(Uri uri, CancellationToken cancellationToken = default)
		{
			var headers = await CreateHeaders();

			return await ExecAsync(() => client.PostAsync(uri, headers, cancellationToken));
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
		internal async Task<RestResponse<EmptyResponse>> UploadAsync(Uri uri, UploadDocumentRequest request, CancellationToken cancellationToken = default)
		{
			var headers = await CreateHeaders();

			return await ExecAsync(() => client.UploadAsync(uri, request, headers, cancellationToken));
		}

		internal Uri GetWithQueryString(string url, int? limit, int? offset, string search = null, string email = null, List<string> status = null)
		{
			var qb = new QueryBuilder();

			if (limit.HasValue)
			{
				qb.Add("limit", limit.ToString());
			}

			if (offset.HasValue)
			{
				qb.Add("offset", offset.ToString());
			}

			if (!string.IsNullOrEmpty(search))
			{
				qb.Add("search", search);
			}

			if (!string.IsNullOrEmpty(email))
			{
				qb.Add("email", email);
			}

			if (status?.Any() != false)
			{
				qb.Add("status", status);
			}

			return new Uri(url + qb.ToQueryString());
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
