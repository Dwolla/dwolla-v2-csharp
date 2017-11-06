using System.Threading.Tasks;

namespace ExampleApp.Tasks
{
    [Task("cuc", "Create an Unverified Customer")]
    class Customer_CreateUnverified : BaseTask
    {
        public override async Task Run()
        {
            var rootRes = await Broker.GetRootAsync();
            var createdCustomerUri = await Broker.CreateCustomerAsync(
                rootRes.Links["customers"].Href, "night", $"man-{RandomAlphaNumericString(5)}",
                $"{RandomAlphaNumericString(20)}@example.com");

            var customer = await Broker.GetCustomerAsync(createdCustomerUri);
            WriteLine($"Created {customer.FirstName} {customer.LastName} with email={customer.Email}");
        }
    }
}