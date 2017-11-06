using System.Collections.Generic;

namespace Dwolla.Client.Models.Responses
{
    public class IavAccountHolders
    {
        public string Selected { get; set; }
        public List<string> Other { get; set; }
    }
}