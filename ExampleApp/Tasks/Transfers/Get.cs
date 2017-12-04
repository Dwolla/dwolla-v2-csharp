using System.Threading.Tasks;

namespace ExampleApp.Tasks.Transfers
{
    [Task("gt", "Get Transfer")]
    class Get : BaseTask
    {
        public override async Task Run()
        {
            Write("Transfer ID to retrieve: ");
            var input = ReadLine();

            var transfer = await Broker.GetTransferAsync(input);

            WriteLine($"Status: {transfer.Status}; Amount: {transfer.Amount.Value} {transfer.Amount.Currency};");
        }
    }
}