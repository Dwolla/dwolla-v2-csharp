using System;
using System.Threading.Tasks;

namespace ExampleApp.Tasks.LabelLedgerEntries
{
    [Task("llle", "List Label Ledger entries for a Label")]
    internal class List : BaseTask
    {
        public override async Task Run()
        {
            Write("Label ID for which to list the Label Ledger entries: ");
            var input = ReadLine();

            var res = await Broker.GetLabelLedgerEntriesAsync(input);
            res.Embedded.LedgerEntries
                .ForEach(le => WriteLine($" - ID: {le.Id}; Amount: {le.Amount.Value} {le.Amount.Currency}"));
        }
    }
}