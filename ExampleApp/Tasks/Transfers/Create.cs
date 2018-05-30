using System;
using System.Threading.Tasks;

namespace ExampleApp.Tasks.Transfers
{
    [Task("ct", "Create Transfer")]
    internal class Create : BaseTask
    {
        public override async Task Run()
        {
            Write("Funding Source ID from which to transfer: ");
            var sourceFundingSource = ReadLine();
            Write("Funding Source ID to which to transfer: ");
            var destinationFundingSource = ReadLine();

            Write("Include a fee? (y/n): ");
            var includeFee = ReadLine();

            Uri uri;
            if ("y".Equals(includeFee, StringComparison.CurrentCultureIgnoreCase))
            {
                var fundingSource = await Broker.GetFundingSourceAsync(destinationFundingSource);
                uri = await Broker.CreateTransferAsync(sourceFundingSource, destinationFundingSource, 50, 1,
                    fundingSource.Links["customer"].Href);
            }
            else
            {
                uri = await Broker.CreateTransferAsync(sourceFundingSource, destinationFundingSource, 50, null, null);
            }

            if (uri == null) return;

            var transfer = await Broker.GetTransferAsync(uri);
            WriteLine($"Created {transfer.Id}; Status: {transfer.Status}");
        }
    }
}