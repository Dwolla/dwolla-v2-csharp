using System;

namespace Dwolla.Client.Models.Responses
{
    public class MicroDepositsResponse : BaseResponse
    {
        public DateTime Created { get; set; }
        public string Status { get; set; }
        public Failure Failure { get; set; }
    }
}