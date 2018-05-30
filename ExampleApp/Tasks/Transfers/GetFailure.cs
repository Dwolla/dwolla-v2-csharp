using System.Threading.Tasks;

namespace ExampleApp.Tasks.Transfers
{
    [Task("gtf", "Get Transfer Failure")]
    internal class GetFailure : BaseTask
    {
        public override async Task Run()
        {
            Write("Transfer ID to retrieve the failure: ");
            var input = ReadLine();

            var failure = await Broker.GetTransferFailureAsync(input);

            WriteLine($"Code: {failure.Code}; Description: {failure.Description};");
        }
    }
}