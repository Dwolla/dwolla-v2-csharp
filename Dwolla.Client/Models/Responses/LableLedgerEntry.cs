using System;

namespace Dwolla.Client.Models.Responses
{
    public class LabelLedgerEntry : BaseResponse
    {
        public string Id { get; set; }
        public Money Amount { get; set; }
        public DateTime Created { get; set; }
    }
}