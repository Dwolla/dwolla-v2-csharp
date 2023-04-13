using Dwolla.Client.HttpRequestServices;
using Dwolla.Client.HttpServices.Architecture;
using Dwolla.Client.Models;
using Dwolla.Client.Models.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dwolla.Client.HttpServices
{
	public class DwollaHttpService
	{
		private IDwollaClient _client;
		public IDwollaClient Client
		{
			get => _client ?? DwollaConfiguration.Client;
			set => _client = value;
		}
		
		private static TokenResponse _cachedToken;
		private static DateTime _expiresAtUtc;

		private readonly Func<Task<string>> _getAccessTokenAsync;

		private async Task<string> GetAccessTokenAsync()
		{
			if (_cachedToken != null && _expiresAtUtc > DateTime.UtcNow)
			{ 
				return _cachedToken.Token;
			}

			var response = await Authorization.GetToken();

			if (response.Error != null)
			{
				throw new Exception(response.Error.Message);
			}

			_cachedToken = response.Content;
			_expiresAtUtc = DateTime.UtcNow.AddSeconds(response.Content.ExpiresIn);

			return _cachedToken.Token;
		}

		public DwollaHttpService() 
			: this(null, null) { }

		public DwollaHttpService(Func<Task<string>> getAccessTokenAsync)
			: this(null, getAccessTokenAsync) { }

		public DwollaHttpService(IDwollaClient dwollaClient)
			: this(dwollaClient, null) { }

		public DwollaHttpService(IDwollaClient dwollaClient, Func<Task<string>> getAccessTokenAsync)
		{
			_getAccessTokenAsync = getAccessTokenAsync;

			if (_getAccessTokenAsync == null)
			{
				_getAccessTokenAsync = GetAccessTokenAsync;
			}

			_client = dwollaClient;
		}

		public async Task AccessTokenAsync()
		{
			await _getAccessTokenAsync();
		}

		private AuthorizationHttpService _authorization;
		public AuthorizationHttpService Authorization
		{
			get 
			{
				if (_authorization == null)
				{
					_authorization = new AuthorizationHttpService(Client, _getAccessTokenAsync);
				}

				return _authorization;
			}
		}

		private MicroDepositsHttpService _microDeposits;
		public MicroDepositsHttpService MicroDeposits
		{
			get
			{
				if (_microDeposits == null)
				{
					_microDeposits = new MicroDepositsHttpService(Client, _getAccessTokenAsync);
				}

				return _microDeposits;
			}
		}

		public CustomersHttpService _customers;
		public CustomersHttpService Customers
		{
			get
			{
				if (_customers == null)
				{
					_customers = new CustomersHttpService(Client, _getAccessTokenAsync);
				}

				return _customers;
			}
		}

		public RootHttpService _root;
		public RootHttpService Root 
		{
			get
			{
				if (_root == null)
				{
					return new RootHttpService(Client, _getAccessTokenAsync); 
				}

				return _root;
			}
		}

		// TODO: Implement the rest of the resources
	}
}
