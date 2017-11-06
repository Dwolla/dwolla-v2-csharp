using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dwolla.Client.Models.Responses
{
    public class GetCustomersResponse : BaseGetResponse<Customer>
    {
        [JsonProperty(PropertyName = "_embedded")]
        public new CustomersEmbed Embedded { get; set; }
    }

    public class CustomersEmbed : Embed<Customer>
    {
        public List<Customer> Customers { get; set; }

        public override List<Customer> Results() => Customers;
    }
}