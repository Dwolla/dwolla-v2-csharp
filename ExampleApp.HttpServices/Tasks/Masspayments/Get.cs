using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.Masspayments
{
    [Task("gmp", "Get Masspayment")]
    internal class Get : BaseTask
    {
        public override async Task Run()
        {
            Write("Masspayment ID to retrieve: ");
            var input = ReadLine();

            var response = await HttpService.MassPayments.GetMassPaymentAsync(input);

            WriteLine($"Created {response.Content.Id}: Status - {response.Content.Status} | Total - {response.Content.Total.Value} {response.Content.Total.Currency} | Created - {response.Content.Created}");
        }
    }
}