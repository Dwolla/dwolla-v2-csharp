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

            if (response.Error is not null)
            {
                WriteLine($"Response {response.Response.StatusCode}. Error creating label: {response.Error.Message}.");
            }
            else if (response.Response.Headers?.Location is not null)
            {
                var labelResponse = await HttpService.Labels.GetLabelAsync(response.Response.Headers.Location.ToString().Split('/').Last());

                WriteLine(
                    labelResponse.Error is not null
                        ? $"Label was created. However, an error occurred while retrieving the label. {labelResponse.Error}"
                        : $"Created: {labelResponse.Content.Id}");
            }
            else
            {
                WriteLine("Label created successfully. But no resource URI was provided.");
            }
        }
    }
}