using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.Labels
{
    [Task("llle", "List Label Ledger entries for a Label")]
    internal class ListLabelLedger : BaseTask
    {
        public override async Task Run()
        {
            Write("Label ID for which to list the Label Ledger entries: ");
            var input = ReadLine();

            var response = await HttpService.Labels.GetLedgerEntryCollectionAsync(input, null, null);

            if (response.Error is not null)
            {
                WriteLine($"Error retrieving label ledge entries. {response.Error.Message}.");
            }
            else
            {
                response.Content.Embedded.LedgerEntries
                    .ForEach(le => WriteLine($"Label Ledger Entry: ID - {le.Id} | Amount - {le.Amount.Value} {le.Amount.Currency}"));
            }
        }
    }
}