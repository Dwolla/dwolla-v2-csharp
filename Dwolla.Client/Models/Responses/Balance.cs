using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dwolla.Client.Models.Responses
{
    public class Balance
    {
        public decimal Value { get; set; }
        public string Currency { get; set; }
    }
}