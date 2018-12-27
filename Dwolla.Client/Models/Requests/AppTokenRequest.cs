namespace Dwolla.Client.Models.Requests
{
    public class AppTokenRequest
    {
        public string Key { get; set; }
        public string Secret { get; set; }
        public string GrantType => "client_credentials";
    }
}