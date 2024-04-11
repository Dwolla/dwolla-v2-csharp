using System;
using System.Text.Json.Serialization;

namespace Dwolla.Client.Models.Responses
{
    public class TransferResponse : BaseResponse
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public Money Amount { get; set; }
        public DateTime Created { get; set; }
        public Clearing Clearing { get; set; }
    }
}