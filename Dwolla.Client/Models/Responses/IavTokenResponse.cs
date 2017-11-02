using Newtonsoft.Json;

namespace Dwolla.Client.Models.Responses
{
    public class IavTokenResponse : RootResponse
    {
        public string Token { get; set; }
    }
}