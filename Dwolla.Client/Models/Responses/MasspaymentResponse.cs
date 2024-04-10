using System;
using System.Collections.Generic;

namespace Dwolla.Client.Models.Responses
{
    public class MasspaymentResponse : BaseResponse
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
    }
}