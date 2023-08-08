using Dwolla.Client.Models.Requests;
using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.Customers
{
    [Task("cu", "Update Customer")]
    internal class Update : BaseTask
    {
        public override async Task Run()
        {
            Write("Customer ID for whom to update: ");
            var input = ReadLine();

            var response = await HttpService.Customers.UpdateCustomerAsync(input, new UpdateCustomerRequest { Status = "deactivated" });

            WriteLine($"Customer updated: Status - {response.Content.Status}");
        }
    }
}