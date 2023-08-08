using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.FundingSources
{
    [Task("gmd", "Get Micro-deposits")]
    internal class GetMicroDeposit : BaseTask
    {
        public override async Task Run()
        {
            Write("Funding Source ID for which to get the micro-deposits: ");
            var input = ReadLine();

            var response = await HttpService.FundingSources.GetMicroDepositAsync(input);
            WriteLine($"Status: {response.Content.Status}");
        }
    }
}