using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dwolla.Client.Models.Responses
{
    public class ExchangeResponse : BaseResponse
    {
        [JsonProperty(PropertyName = "_links")]
        public Dictionary<string, Link> Links { get; set; }
        public string Id { get; set; }
        public DateTime Created { get; set; }
        public string Status { get; set; }
    }
}