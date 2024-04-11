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

            var response = await HttpService.FundingSources.VerifyMicroDepositAsync(
                fundingSourceId,
                new MicroDepositsRequest
                {
                    Amount1 = new Money { Value = .05m, Currency = "USD" },
                    Amount2 = new Money { Value = .05m, Currency = "USD" }
                }
            );

            if (response == null) return;

            if (response.Error is not null)
            {
                WriteLine($"Error verifying micro deposit: {response.Error.Message}.");
                if (response.Error.Embedded is not null && response.Error.Embedded.Errors.Any())
                {
                    WriteLine("  Errors:");
                    foreach (var error in response.Error.Embedded.Errors)
                    {
                        WriteLine("    - " + error.Code + ": " + error.Message);
                    }
                    WriteLine("");
                }
            }
            else
            {
                WriteLine($"Micro-deposit Verified");
            }
        }
    }
}