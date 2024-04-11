using System.Text.Json.Serialization;

namespace Dwolla.Client.Models
{
    public class Money
    {
        [JsonConverter(typeof(DecimalFormatConverter))]
        public decimal Value { get; set; }
        
        public string Currency { get; set; }
    }
}