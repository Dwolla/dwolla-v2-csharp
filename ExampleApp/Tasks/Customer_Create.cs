using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExampleApp.Tasks
{
    [Task("cc", "Create a Customer")]
    class Customer_Create : BaseTask
    {
        public override async Task Run()
        {
            var rootRes = await Broker.GetRootAsync();
            var createdCustomerUri = await Broker.CreateCustomerAsync(
                rootRes.Links["customers"].Href, "night", "man", $"{RandomString(20)}@example.com");

            var customer = await Broker.GetCustomerAsync(createdCustomerUri);
            WriteLine($"Created {customer.FirstName} {customer.LastName} with email={customer.Email}");
        }
    }
}
