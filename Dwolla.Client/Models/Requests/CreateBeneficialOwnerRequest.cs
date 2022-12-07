using System;

namespace Dwolla.Client.Models.Requests
{
    public class CreateBeneficialOwnerRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // TODO: [JsonConverter(typeof(DateOnlyConverter))] 
        public DateTime DateOfBirth { get; set; }

        public string Ssn { get; set; }
        public Address Address { get; set; }
        public Passport Passport { get; set; }
    }
}