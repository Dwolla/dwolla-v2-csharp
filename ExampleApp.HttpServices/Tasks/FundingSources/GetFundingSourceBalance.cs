using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.FundingSources;

[Task("gfsb", "Get Funding Source Balance")]
internal class GetFundingSourceBalance: BaseTask
{
    public override async Task Run()
    {
        Write("Funding Source ID for which to get balance: ");
        var input = ReadLine();

        var response = await HttpService.FundingSources.GetFundingSourceBalanceAsync(input);

        WriteLine(response.Error is not null
            ? $"Error retrieving funding source balance: {response.Error.Message}."
            : $"Funding source balance: {response.Content.Balance}. Total: {response.Content.Total}.");
    }
}