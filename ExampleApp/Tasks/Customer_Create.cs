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
                rootRes.Links["customers"].Href, "night", $"man-{RandomString(5)}", $"{RandomString(20)}@example.com", new DateTime(1980, 1, 1));

            var customer = await Broker.GetCustomerAsync(createdCustomerUri);
            WriteLine($"Created {customer.FirstName} {customer.LastName} with email={customer.Email}");
        }
    }
}
