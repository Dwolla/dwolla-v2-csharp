using Dwolla.Client.Models;
using Dwolla.Client.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.Labels
{
    [Task("clr", "Create Label Reallocation")]
    internal class CreateLabelReallocation : BaseTask
    {
        public override async Task Run()
        {
            Write("From Label ID: ");
            var fromLabelId = ReadLine();

            Write("To Label ID: ");
            var toLabelId = ReadLine();

            WriteLine("Amount to reallocate: ");

            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                WriteLine("Amount entered is not a decimal. Please enter a correct value.");
                return;
            }

            var response = await HttpService.Labels.CreateLabelReallocationAsync(
                 new CreateLabelReallocationRequest
                 {
                     Amount = new Money
                     {
                         Currency = "USD",
                         Value = amount
                     },
                     Links = new Dictionary<string, Link>
                    {
                        {"from", new Link {Href = new Uri($"https://api-sandbox.dwolla.com//labels/{fromLabelId}")}},
                        {"to", new Link {Href = new Uri($"https://api-sandbox.dwolla.com//labels/{toLabelId}")}}
                    }
                 });

            if (response == null) return;

            var entryResponse = await HttpService.Labels.GetReallocationAsync(response.Response.Headers.Location.ToString().Split('/').Last());

            WriteLine($"Created: {entryResponse.Content.Created}");
        }
    }
}