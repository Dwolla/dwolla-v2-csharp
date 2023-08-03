using System.Linq;
using System.Threading.Tasks;
using Dwolla.Client.Models.Requests;

namespace ExampleApp.HttpServices.Tasks.FundingSources
{
    [Task("cfs", "Create a Funding Source for a Customer")]
    internal class Create : BaseTask
    {
        public override async Task Run()
        {
            Write("Customer ID for whom to create funding source: ");
            var input = ReadLine();

            var response = await HttpService.FundingSources.CreateFundingSourceAsync(
                input,
                new CreateFundingSourceRequest
                {
                    Name = $"Nickname-{RandomNumericString(5)}",
                    AccountNumber = $"{RandomNumericString(9)}",
                    RoutingNumber = "222222226",
                    BankAccountType = "checking",
                });

            if (response == null) return;

            var fundingSourceResponse = await HttpService.FundingSources.GetFundingSourceAsync(response.Response.Headers.Location.ToString().Split('/').Last());
            WriteLine($"Created funding-source: {fundingSourceResponse.Content.Name}");
        }
    }
}