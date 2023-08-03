using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.Customers
{
    [Task("lc", "List Customers")]
    internal class List : BaseTask
    {
        public override async Task Run()
        {
            var response = await HttpService.Customers.GetCustomerCollectionAsync(string.Empty, string.Empty, new List<string>(), null, null);

            response.Content.Embedded.Customers
                .ForEach(c => WriteLine($"Customer: {c.Id} - {c.FirstName} - {c.LastName}"));
        }
    }
}