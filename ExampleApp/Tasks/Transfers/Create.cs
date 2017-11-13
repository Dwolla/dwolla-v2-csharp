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

            var transferUri = await Broker.CreateTransferAsync(sourceFundingSource, destinationFundingSource, 1);

            var transfer = await Broker.GetTransferAsync(transferUri);
            WriteLine($"Created {transfer.Id}; Status: {transfer.Status}");
        }
    }
}