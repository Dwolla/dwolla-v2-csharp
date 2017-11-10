using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dwolla.Client.Models.Responses
{
    public class BalanceResponse : BaseResponse
    {
        public Money Balance { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string Status { get; set; }
    }

    public class Money
    {
        public decimal Value { get; set; }
        public string Currency { get; set; }
    }
}