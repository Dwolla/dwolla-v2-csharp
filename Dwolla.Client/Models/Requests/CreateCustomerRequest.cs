using System;

namespace Dwolla.Client.Models.Requests
{
    public class CreateCustomerRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
        public string IpAddress { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Ssn { get; set; }
        public string Phone { get; set; }
        public string BusinessName { get; set; }
        public string BusinessType { get; set; }
        public string BusinessClassification { get; set; }
        public string Ein { get; set; }
        public string DoingBusinessAs { get; set; }
        public string Website { get; set; }
        public Controller Controller { get; set; }
    }
}