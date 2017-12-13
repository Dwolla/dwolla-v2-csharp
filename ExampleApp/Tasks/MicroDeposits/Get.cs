using System.Threading.Tasks;

namespace ExampleApp.Tasks.MicroDeposits
{
    [Task("gmd", "Get Micro-deposits")]
    class Get : BaseTask
    {
        public override async Task Run()
        {
            Write("Funding Source ID for which to get the micro-deposits: ");
            var input = ReadLine();

            var res = await Broker.GetMicroDepositsAsync(input);
            WriteLine($"Status: {res.Status}");
        }
    }
}