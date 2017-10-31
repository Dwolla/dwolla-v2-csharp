using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dwolla.Client.Models.Responses
{
    public class IavAccountHolders
    {
        public string Selected { get; set; }
        public string[] Other { get; set; }
    }
}