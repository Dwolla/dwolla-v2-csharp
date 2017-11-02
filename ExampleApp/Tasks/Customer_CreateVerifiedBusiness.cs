using Dwolla.Client.Models.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExampleApp.Tasks
{
    [Task("cvbc", "Create a Verified Business Customer")]
    class Customer_CreateVerifiedBusiness : BaseTask
    {
        public override async Task Run()
        {
            var rootRes = await Broker.GetRootAsync();
            var createdCustomerUri = await Broker.CreateCustomerAsync(rootRes.Links["customers"].Href, new CreateCustomerRequest()
            {
                FirstName = "night",
                LastName = "businessman",
                Email = $"{RandomString(20)}@example.com",
                Type = "business",
	            Address1 = "123 Sesame St",
	            City = "SmallTown",
	            State = "VA",
	            PostalCode = "22222",
	            DateOfBirth = new DateTime(1980, 1, 1),
	            Ssn = RandomNumberString(4),
	            Phone = "1234567890",
	            BusinessName = "Test Business",
	            BusinessType = "corporation",
	            BusinessClassification = "9ed3f67a-7d6f-11e3-bb5b-5404a6144203",
	            Ein = RandomNumberString(9)
            });

            var customer = await Broker.GetCustomerAsync(createdCustomerUri);
            WriteLine($"Created {customer.FirstName} {customer.LastName} at {customer.BusinessName} with email={customer.Email}");
        }
    }
}
