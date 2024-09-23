using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.FundingSources;

[Task("gfsb", "Get Funding Sources for Customer")]
internal class GetFundingSourceForCustomer : BaseTask
{
    public override async Task Run()
    {
        Write("Customer ID of funding sources to retrieve: ");
        var input = ReadLine();

        var response = await HttpService.FundingSources.GetFundingSourceForCustomerAsync(input);

        if (response.Error is not null)
        {
            WriteLine($"Error retrieving funding sources for customer. {response.Error.Message}.");
        }
        else
        {
            response.Content.Embedded.FundingSources.ForEach(l => WriteLine($"Funding source: ID - {l.Id}; Name - {l.Name}; Status - {l.Status}"));
        }
    }
}
