using System;
using System.Collections.Generic;

namespace Dwolla.Client.Models.Responses
{
    public class TransferResponse : BaseResponse
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public Money Amount { get; set; }
        public DateTime Created { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public Clearing Clearing { get; set; }
        public AchDetails AchDetails {get; set; }
        public string CorrelationId { get; set; }
    }
}