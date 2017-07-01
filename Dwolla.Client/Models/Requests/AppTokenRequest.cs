using Newtonsoft.Json;

namespace Dwolla.Client.Models.Requests
{
    public class AppTokenRequest
    {
        [JsonProperty(PropertyName = "client_id")]
        public string Key { get; set; }

        [JsonProperty(PropertyName = "client_secret")]
        public string Secret { get; set; }

        [JsonProperty(PropertyName = "grant_type")]
        public string GrantType => "client_credentials";
    }
}