using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.Customers
{
    [Task("gcit", "Get a Customer IAV Token")]
    internal class IavToken : BaseTask
    {
        public override async Task Run()
        {
            Write("Customer ID for whom to get an IAV token: ");
            var input = ReadLine();

            var response = await HttpService.Customers.GetCustomerIavTokenAsync(input);
            WriteLine($"Token created: {response.Content.Token}");
        }
    }
}