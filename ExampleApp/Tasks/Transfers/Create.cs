using System;
using System.Threading.Tasks;

namespace ExampleApp.Tasks.Transfers
{
    [Task("ct", "Create Transfer")]
    class Create : BaseTask
    {
        public override async Task Run()
        {
            Write("Funding Source ID from which to transfer: ");
            var sourceFundingSource = ReadLine();
            Write("Funding Source ID to which to transfer: ");
            var destinationFundingSource = ReadLine();

            Write("Include a fee? (y/n): ");
            var includeFee = ReadLine();

            Uri transferUri;
            if ("y".Equals(includeFee, StringComparison.CurrentCultureIgnoreCase))
            {
                var fundingSource = await Broker.GetFundingSourceAsync(destinationFundingSource);
                transferUri = await Broker.CreateTransferAsync(sourceFundingSource, destinationFundingSource, 50, 1, fundingSource.Links["customer"].Href);
            }
            else
            {
                transferUri = await Broker.CreateTransferAsync(sourceFundingSource, destinationFundingSource, 50, null, null);
            }

            if (transferUri == null)
                throw new Exception("transfer failed");

            var transfer = await Broker.GetTransferAsync(transferUri);
            WriteLine($"Created {transfer.Id}; Status: {transfer.Status}");
        }
    }
}