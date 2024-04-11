using System;
using System.Text.Json.Serialization;

namespace Dwolla.Client.Models.Responses
{
    public class GetFundingSourceBalanceResponse : BaseResponse
    { 
        [JsonPropertyName("balance")] 
        public Money Balance { get; set; }
        
        [JsonPropertyName("total")] 
        public Money Total { get; set; }
            
        [JsonPropertyName("lastUpdated")] 
        public DateTime LastUpdated { get; set; }
    }
}