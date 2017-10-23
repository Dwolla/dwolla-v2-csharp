using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dwolla.Client.Models.Responses
{
    public class GetCustomersResponse
    {
        [JsonProperty(PropertyName = "_links")]
        public Dictionary<string, Link> Links { get; set; }

        [JsonProperty(PropertyName = "_embedded")]
        public CustomersEmbed Embedded { get; set; }

        public int Total { get; set; }
    }

    public class CustomersEmbed
    {
        public List<Customer> Customers { get; set; }
    }
}