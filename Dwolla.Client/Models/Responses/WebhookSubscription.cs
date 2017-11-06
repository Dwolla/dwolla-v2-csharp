using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dwolla.Client.Models.Responses
{
    public class WebhookSubscription : BaseResponse
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public bool Paused { get; set; }
        public DateTime Created { get; set; }
    }
}