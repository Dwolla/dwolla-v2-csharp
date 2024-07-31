using Dwolla.Client.Models;
using Dwolla.Client.Models.Requests;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.Labels
{
    [Task("clle", "Create Label Ledger Entry")]
    internal class CreateLabelLedger : BaseTask
    {
        public override async Task Run()
        {
            Write("Label ID for which to create a label ledger entry: ");
            var input = ReadLine();

            Write("Amount of funds to increase or decrease: ");

            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                WriteLine("Amount entered is not a decimal. Please enter a correct value.");
                return;
            }

            var response = await HttpService.Labels.CreateLedgerEntryAsync(
                input,
                new CreateLabelLedgerEntryRequest { Amount = new Money { Currency = "USD", Value = amount } }
             );

            if (response == null) return;

            if (response.Error is not null)
            {
                WriteLine($"Response {response.Response.StatusCode}. Error creating label: {response.Error.Message}.");
            }
            else if (response.Response.Headers?.Location is not null)
            {
                var entryResponse = await HttpService.Labels.GetLedgerEntryAsync(response.Response.Headers.Location.ToString().Split('/').Last());

                WriteLine($"Created {entryResponse.Content.Id}: Amount {entryResponse.Content.Amount.Value} {entryResponse.Content.Amount.Currency} ");
            }
            else
            {
                WriteLine("Label created successfully. But no resource URI was provided.");
            }
        }
    }
}