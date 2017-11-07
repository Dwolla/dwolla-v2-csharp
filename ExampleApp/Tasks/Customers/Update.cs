using System;
using System.Threading.Tasks;
using Dwolla.Client.Models.Requests;

namespace ExampleApp.Tasks.Customers
{
    [Task("cu", "Update Customer")]
    class Update : BaseTask
    {
        public override async Task Run()
        {
            Write("Customer ID for whom to update: ");
            var input = ReadLine();

            var rootRes = await Broker.GetRootAsync();
            var res = await Broker.UpdateCustomerAsync(new Uri($"{rootRes.Links["customers"].Href}/{input}"),
                new UpdateCustomerRequest {Status = "deactivated"});

            WriteLine($"Customer updated: Status={res.Status}");
        }
    }
}