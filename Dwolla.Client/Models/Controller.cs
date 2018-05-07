using System;

namespace Dwolla.Client.Models
{
    public class Controller
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public Address Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Ssn { get; set; }
    }
}