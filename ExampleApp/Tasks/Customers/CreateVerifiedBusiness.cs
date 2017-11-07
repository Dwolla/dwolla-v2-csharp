using System;
using System.Threading.Tasks;
using Dwolla.Client.Models.Requests;

namespace ExampleApp.Tasks.Customers
{
    [Task("cvbc", "Create a Verified Business Customer")]
    class CreateVerifiedBusiness : BaseTask
    {
        public override async Task Run()
        {
            var rootRes = await Broker.GetRootAsync();
            var createdCustomerUri = await Broker.CreateCustomerAsync(rootRes.Links["customers"].Href,
                new CreateCustomerRequest()
                {
                    FirstName = "night",
                    LastName = "businessman",
                    Email = $"{RandomAlphaNumericString(20)}@example.com",
                    Type = "business",
                    Address1 = "123 Sesame St",
                    City = "SmallTown",
                    State = "VA",
                    PostalCode = "22222",
                    DateOfBirth = new DateTime(1980, 1, 1),
                    Ssn = RandomNumericString(4),
                    Phone = "1234567890",
                    BusinessName = "Test Business",
                    BusinessType = "corporation",
                    BusinessClassification = "9ed3f67a-7d6f-11e3-bb5b-5404a6144203",
                    Ein = RandomNumericString(9)
                });

            var customer = await Broker.GetCustomerAsync(createdCustomerUri);
            WriteLine(
                $"Created {customer.FirstName} {customer.LastName} at {customer.BusinessName} with email={customer.Email}");
        }
    }
}