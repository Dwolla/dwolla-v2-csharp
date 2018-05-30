using System.Threading.Tasks;

namespace ExampleApp.Tasks.Customers
{
    [Task("cuc", "Create an Unverified Customer")]
    internal class CreateUnverified : BaseTask
    {
        public override async Task Run()
        {
            var rootRes = await Broker.GetRootAsync();
            var uri = await Broker.CreateCustomerAsync(
                rootRes.Links["customers"].Href, "night", $"man-{RandomAlphaNumericString(5)}",
                $"{RandomAlphaNumericString(20)}@example.com");

            if (uri == null) return;

            var customer = await Broker.GetCustomerAsync(uri);
            WriteLine($"Created {customer.FirstName} {customer.LastName} with email={customer.Email}");
        }
    }
}