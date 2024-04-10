using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.Transfers
{
    [Task("gt", "Get Transfer")]
    internal class Get : BaseTask
    {
        public override async Task Run()
        {
            Write("Transfer ID to retrieve: ");
            var input = ReadLine();

            var response = await HttpService.Transfers.GetTransferAsync(input);

            WriteLine(response.Error is not null ? $"Error when retrieving transfer. {response.Error.Message}." :
                $"Status: {response.Content.Status} | Amount - {response.Content.Amount.Value} {response.Content.Amount.Currency};");
        }
    }
}