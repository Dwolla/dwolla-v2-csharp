using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.Customers
{
    [Task("gc", "Get Customer")]
    internal class Get : BaseTask
    {
        public override async Task Run()
        {
            Write("Customer ID for whom to retreive: ");
            var input = ReadLine();

            var response = await HttpService.Customers.GetCustomerAsync(input);

            WriteLine($"Customer: {response.Content.Id} - {response.Content.FirstName} - {response.Content.LastName}");
        }
    }
}