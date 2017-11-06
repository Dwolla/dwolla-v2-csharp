using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dwolla.Client.Models.Responses
{
    public class BalanceResponse : BaseResponse
    {
        public Balance Balance { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}