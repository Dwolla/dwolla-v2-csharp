using System;
using System.Threading.Tasks;
using Dwolla.Client.Models;
using Dwolla.Client.Models.Requests;

namespace ExampleApp.Tasks.LabelLedgerEntries
{
    [Task("clle", "Create Label Ledger Entry")]
    internal class Create : BaseTask
    {
        public override async Task Run()
        {
            Write("Label ID for which to create a label ledger entry: ");
            var input = ReadLine();

            // Read String input and convert to Decimal
            decimal amount = 0;
            Write("Amount of funds to increase or decrease: ");
            try
            {
                amount = Convert.ToDecimal(Console.ReadLine());
            }
            catch (System.OverflowException) {}
            catch (System.FormatException) {}
            catch (System.ArgumentNullException) {}

            var uri = await Broker.CreateLabelLedgerEntryAsync(input, amount);

            if (uri == null) return;

            var entry = await Broker.GetLabelLedgerEntryAsync(uri);
            WriteLine($"Created {entry.Id}; Amount {entry.Amount.Value} {entry.Amount.Currency} ");
        }
    }
}