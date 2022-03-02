using System;
using System.Threading.Tasks;
using Dwolla.Client.Models.Requests;

namespace ExampleApp.Tasks.FundingSources
{
    [Task("cfs", "Create a Funding Source for a Customer")]
    internal class Create : BaseTask
    {
        public override async Task Run()
        {
            Write("Customer ID for whom to create funding source: ");
            var input = ReadLine();

            var rootRes = await Broker.GetRootAsync();
            var uri = await Broker.CreateFundingSourceAsync(
                new Uri($"{rootRes.Links["customers"].Href}/{input}/funding-sources"),
                new CreateFundingSourceRequest
                {
                    Name = $"Nickname-{RandomNumericString(5)}",
                    AccountNumber = $"{RandomNumericString(9)}",
                    RoutingNumber = "222222226",
                    BankAccountType = "checking",
                });

            if (uri == null) return;

            var fundingSource = await Broker.GetFundingSourceAsync(uri);
            WriteLine($"Created funding-source {fundingSource.Name}");
        }
    }
}