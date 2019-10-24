namespace Dwolla.Client.Models.Requests {
    public class AuthorizationCodeRequest {
        public string Key { get; set; }
        public string Secret { get; set; }
        public string Code { get; set; }
        public string RedirectUri { get; set; }
        public string GrantType => "authorization_code";
    }
}
