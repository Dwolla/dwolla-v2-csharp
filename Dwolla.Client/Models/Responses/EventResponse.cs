using System;

namespace Dwolla.Client.Models.Responses
{
    public class EventResponse : BaseResponse
    {
        public string Id { get; set; }
        public DateTime Created { get; set; }
        public string Topic { get; set; }
        public string ResourceId { get; set; }
    }
}