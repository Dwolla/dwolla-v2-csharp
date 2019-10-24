namespace Dwolla.Client.Models.Requests {
    public class RefreshTokenRequest {
        public string Key { get; set; }
        public string Secret { get; set; }
        public string RefreshToken { get; set; }
        public string GrantType => "refresh_token";
    }
}
