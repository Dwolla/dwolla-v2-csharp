using System;
using System.Threading.Tasks;

namespace ExampleApp.Tasks.MicroDeposits
{
    [Task("vmd", "Verify Micro-deposits")]
    internal class Verify : BaseTask
    {
        public override async Task Run()
        {
            Write("Funding Source ID for which to verify the micro-deposits: ");
            var fundingSource = ReadLine();

            Write("First micro-deposit amount: ");
            var amount1 = decimal.Parse(ReadLine());

            Write("Second micro-deposit amount: ");
            var amount2 = decimal.Parse(ReadLine());

            var uri = await Broker.VerifyMicroDepositsAsync(fundingSource, amount1, amount2);

            if (uri == null) return;

            Console.WriteLine("Verified");
        }
    }
}