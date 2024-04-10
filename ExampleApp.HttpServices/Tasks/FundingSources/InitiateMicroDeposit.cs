using System.Linq;
using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.FundingSources;

[Task("imd", "Initiate Micro-deposits")]
internal class InitiateMicroDeposit : BaseTask
{
      public override async Task Run()
      {
          Write("Funding Source ID for which to initiate the micro-deposits: ");
          var input = ReadLine();

          var response = await HttpService.FundingSources.InitiateMicroDepositAsync(input);

          WriteLine(response.Error is not null
              ? $"Error retrieving micro deposits: {response.Error.Message}."
              : "Micro deposit was successfully created.");
      }
}