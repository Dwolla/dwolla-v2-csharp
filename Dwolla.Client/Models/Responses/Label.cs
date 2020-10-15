using System;

namespace Dwolla.Client.Models.Responses
{
    public class Label : BaseResponse
    {
        public string Id { get; set; }
        public DateTime Created { get; set; }
        public Money Amount { get; set; }
    }
}