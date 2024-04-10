using System.Linq;
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
            
            if (response.Error is not null)
            {
                WriteLine($"Error retrieving micro deposits: {response.Error.Message}.");
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
                WriteLine($"Status: {response.Content.Status}");
            }
        }
    }
}