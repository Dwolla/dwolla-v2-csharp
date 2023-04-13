using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.MicroDeposits
{
    [Task("gmd", "Get Micro-deposits")]
    internal class Get : BaseTask
    {
        public override async Task Run()
        {
            Write("Funding Source ID for which to get the micro-deposits: ");
            var input = ReadLine();

            var res = await HttpService.MicroDeposits.GetAsync(input);
            WriteLine($"Status: {res.Content.Status}");
        }
    }
}