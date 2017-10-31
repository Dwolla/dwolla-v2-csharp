using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dwolla.Client.Models.Responses
{
    public class WebhookSubscription
    {
        [JsonProperty(PropertyName = "_links")]
        public Dictionary<string, Link> Links { get; set; }

        public string Id { get; set; }
        public string Url { get; set; }
        public bool Paused { get; set; }
        public DateTime Created { get; set; }
    }
}