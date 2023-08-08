using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.Labels
{
    [Task("glle", "Get Label Ledger entry")]
    internal class GetLabelLedger : BaseTask
    {
        public override async Task Run()
        {
            Write("Label Ledger entry ID to retrieve: ");
            var input = ReadLine();

            var response = await HttpService.Labels.GetLedgerEntryAsync(input);

            WriteLine($"Amount: {response.Content.Amount.Value} {response.Content.Amount.Currency}");
        }
    }
}