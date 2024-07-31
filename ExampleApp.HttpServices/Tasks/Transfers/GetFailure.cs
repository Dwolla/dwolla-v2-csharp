using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.Transfers
{
    [Task("gtf", "Get Transfer Failure")]
    internal class GetFailure : BaseTask
    {
        public override async Task Run()
        {
            Write("Transfer ID to retrieve the failure: ");
            var input = ReadLine();

            var response = await HttpService.Transfers.GetFailureAsync(input);

            WriteLine($"Code: {response.Content.Code} | Description - {response.Content.Description};");
        }
    }
}