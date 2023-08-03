using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.Customers
{
    [Task("lc", "List Customers")]
    internal class List : BaseTask
    {
        public override async Task Run()
        {
            Write("Limit the amount of customers retreived: ");

            if (!int.TryParse(Console.ReadLine(), out int limit))
            {
                WriteLine("Limit entered is not an int. Please enter a correct value.");
                return;
            }

            Write("Offset customer retreived: ");

            if (!int.TryParse(Console.ReadLine(), out int offset))
            {
                WriteLine("Offset entered is not a int. Please enter a correct value.");
                return;
            }

            Write("Status for customers retreived: ");

            var status = Console.ReadLine();
            var statusArray = status.Split(',', StringSplitOptions.TrimEntries);

            var response = await HttpService.Customers.GetCustomerCollectionAsync(string.Empty, string.Empty, statusArray.Length == 0 ? null : statusArray.ToList(), limit == 0 ? null : limit, offset == 0 ? null : offset);

            response.Content.Embedded.Customers
                .ForEach(c => WriteLine($"Customer: {c.Id} - {c.FirstName} - {c.LastName}"));
        }
    }
}