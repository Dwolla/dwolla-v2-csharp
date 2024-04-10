using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.FundingSources
{
    [Task("gfs", "Get a Funding Source")]
    internal class Get : BaseTask
    {
        public override async Task Run()
        {
            Write("Funding Source ID for whom to retreive: ");
            var input = ReadLine();

            var fundingSourceResponse = await HttpService.FundingSources.GetFundingSourceAsync(input);
            
            WriteLine(fundingSourceResponse.Error is not null ? $"Error retrieving funding source {fundingSourceResponse.Error.Message}" 
                : $"Created funding-source: {fundingSourceResponse.Content.Name}");
        }
    }
}