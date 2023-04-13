namespace Dwolla.Client.HttpServices.Architecture
{
	public static class DwollaConfiguration
	{
		public static bool IsSandbox { get; set; }

		public static string Key { get; set; }
		public static string Secret { get; set; }

		private static IDwollaClient _client;

		public static IDwollaClient Client
		{
			get
			{
				if (_client is null)
				{
					_client = DwollaClient.Create(IsSandbox);
				}

				return _client;
			}
			set
			{
				_client = value;
			}
		}
	}
}
