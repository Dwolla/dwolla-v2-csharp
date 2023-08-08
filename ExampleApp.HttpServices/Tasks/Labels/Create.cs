using Dwolla.Client.Models;
using Dwolla.Client.Models.Requests;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.Labels
{
    [Task("cl", "Create a Label for a Customer")]
    internal class Create : BaseTask
    {
        public override async Task Run()
        {
            Write("Customer ID for whom to create label: ");
            var input = ReadLine();

            Write("Amount to label: ");

            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                WriteLine("Amount entered is not a decimal. Please enter a correct value.");
                return;
            }

            var response = await HttpService.Labels.CreateLabelAsync(
                input,
                new CreateLabelRequest
                {
                    Amount = new Money { Currency = "USD", Value = amount }
                }
            );

            if (response == null) return;

            var ownerResponse = await HttpService.Labels.GetLabelAsync(response.Response.Headers.Location.ToString().Split('/').Last());
            WriteLine($"Created: {ownerResponse.Content.Id} ");
        }
    }
}