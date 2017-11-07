namespace Dwolla.Client.Models.Requests
{
    public class MicroDepositsRequest
    {
        public Amount Amount1 { get; set; }
        public Amount Amount2 { get; set; }
    }

    public class Amount
    {
        public decimal Value { get; set; }
        public string Currency { get; set; }
    }
}