using System;

namespace Dwolla.Client.Models.Responses
{
    public class BalanceResponse : BaseResponse
    {
        public Money Balance { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string Status { get; set; }
    }
}