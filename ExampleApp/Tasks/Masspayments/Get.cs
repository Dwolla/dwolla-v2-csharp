using System.Threading.Tasks;

namespace ExampleApp.Tasks.Masspayments
{
    [Task("gmp", "Get Masspayment")]
    internal class Get : BaseTask
    {
        public override async Task Run()
        {
            Write("Masspayment ID to retrieve: ");
            var input = ReadLine();

            var masspayment = await Broker.GetMasspaymentAsync(input);

            WriteLine($"Created {masspayment.Id}; Status: {masspayment.Status}; Total: {masspayment.Total.Value} {masspayment.Total.Currency}; Created: {masspayment.Created}");
        }
    }
}