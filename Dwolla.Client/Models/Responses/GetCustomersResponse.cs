using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dwolla.Client.Models.Responses
{
    public class GetCustomersResponse
    {
        [JsonProperty(PropertyName = "_links")]
        public Dictionary<string, Link> Links { get; set; }

        [JsonProperty(PropertyName = "_embedded")]
        public CustomerEmbed Embedded { get; set; }

        public int Total { get; set; }
    }

    public class CustomerEmbed
    {
        public List<Customer> Customers { get; set; }
    }
}