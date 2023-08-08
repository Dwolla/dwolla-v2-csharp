using System;

namespace Dwolla.Client.Models.Responses
{
    public class ExchangePartnerResponse : BaseResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
    }
}