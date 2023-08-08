using System;

namespace Dwolla.Client.Models.Responses
{
    public class ExchangeResponse : BaseResponse
    {
        public string Id { get; set; }
        public DateTime Created { get; set; }
        public string Status { get; set; }
    }
}