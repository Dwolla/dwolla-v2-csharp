using System;

namespace Dwolla.Client.Models.Responses
{
    public class Customer : BaseResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string BusinessName { get; set; }
        public string DoingBusinessAs { get; set; }
        public string Website { get; set; }
        public Controller Controller { get; set; }
    }
}