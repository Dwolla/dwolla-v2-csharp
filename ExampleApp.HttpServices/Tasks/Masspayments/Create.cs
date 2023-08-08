using Dwolla.Client.Models;
using Dwolla.Client.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.Masspayments
{
    [Task("cmp", "Create Masspayment")]
    internal class Create : BaseTask
    {
        public override async Task Run()
        {
            Write("Funding Source ID from which to transfer: ");
            var sourceFundingSourceId = ReadLine();

            Write("Destination Funding Source ID for first masspay item: ");
            var destinationFundingSourceId1 = ReadLine();

            if (!decimal.TryParse(Console.ReadLine(), out decimal amount1))
            {
                WriteLine("Amount 1 entered is not a decimal. Please enter a correct value.");
                return;
            }

            Write("Destination Funding Source ID for second masspay item: ");
            var destinationFundingSourceId2 = ReadLine();

            if (!decimal.TryParse(Console.ReadLine(), out decimal amount2))
            {
                WriteLine("Amount 2 entered is not a decimal. Please enter a correct value.");
                return;
            }

            var response = await HttpService.MassPayments.CreateMassPaymentAsync(
                 new CreateMasspaymentRequest
                 {
                     Links = new Dictionary<string, Link>
                    {
                        {"source", new Link {Href = new Uri($"https://api-sandbox.dwolla.com/funding-sources/{sourceFundingSourceId}")}},
                    },
                     Items = new List<MasspaymentItem> {
                      new MasspaymentItem{
                        Links = new Dictionary<string, Link> {{"destination", new Link {Href = new Uri($"https://api-sandbox.dwolla.com/funding-sources/{destinationFundingSourceId1}")}}},
                        Amount= new Money {Value = amount1, Currency = "USD"},
                      },
                      new MasspaymentItem{
                        Links = new Dictionary<string, Link> {{"destination", new Link {Href = new Uri($"https://api-sandbox.dwolla.com/funding-sources/{destinationFundingSourceId2}")}}},
                        Amount= new Money {Value = amount2, Currency = "USD"},
                      }
                   }
                 }
            );

            if (response == null) return;

            var massPaymentResponse = await HttpService.MassPayments.GetMassPaymentAsync(response.Response.Headers.Location.ToString().Split('/').Last());
            WriteLine($"Created {massPaymentResponse.Content.Id}: Status - {massPaymentResponse.Content.Status} | Total - {massPaymentResponse.Content.Total.Value} {massPaymentResponse.Content.Total.Currency} | Created - {massPaymentResponse.Content.Created}");
        }
    }
}