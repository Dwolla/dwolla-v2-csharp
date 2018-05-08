using System;

namespace Dwolla.Client.Models.Responses
{
    public class BeneficialOwnerResponse : BaseResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public string VerificationStatus { get; set; }
    }
}