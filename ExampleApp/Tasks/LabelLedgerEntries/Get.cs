using System.Threading.Tasks;

namespace ExampleApp.Tasks.LabelLedgerEntries
{
    [Task("glle", "Get Label Ledger entry")]
    internal class Get : BaseTask
    {
        public override async Task Run()
        {
            Write("Label Ledger entry ID to retrieve: ");
            var input = ReadLine();

            var labelLedgerEntry = await Broker.GetLabelLedgerEntryAsync(input);

            WriteLine($"Amount: {labelLedgerEntry.Amount.Value} {labelLedgerEntry.Amount.Currency}");
        }
    }
}