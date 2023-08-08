using Dwolla.Client.Models;
using Dwolla.Client.Models.Requests;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.FundingSources
{
    [Task("vmd", "Verify Micro-deposits")]
    internal class Verify : BaseTask
    {
        public override async Task Run()
        {
            Write("Funding Source ID for which to verify the micro-deposits: ");
            var fundingSourceId = ReadLine();

            Write("First micro-deposit amount: ");
            var amount1 = decimal.Parse(ReadLine());

            Write("Second micro-deposit amount: ");
            var amount2 = decimal.Parse(ReadLine());

            var response = await HttpService.FundingSources.VerifyMicroDepositAsync(
                fundingSourceId,
                new MicroDepositsRequest
                {
                    Amount1 = new Money { Value = amount1, Currency = "USD" },
                    Amount2 = new Money { Value = amount2, Currency = "USD" }
                }
            );

            if (response == null) return;

            Console.WriteLine($"Micro-deposit Verified: {response.Response.Headers.Location.ToString().Split('/').Last()}");
        }
    }
}