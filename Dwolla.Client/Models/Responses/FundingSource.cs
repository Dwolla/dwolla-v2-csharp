using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dwolla.Client.Models.Responses
{
    public class FundingSource : BaseResponse
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string BankAccountType { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public bool Removed { get; set; }
        public List<string> Channels { get; set; }
        public string BankName { get; set; }
        public IavAccountHolders IavAccountHolders { get; set; }
        public string Fingerprint { get; set; }
    }
}