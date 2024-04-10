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
            
            WriteLine(response.Error is not null
                ? $"Error when retrieving mass payment. {response.Error.Message}."
                : $"Created {response.Content.Id}: Status - {response.Content.Status} | Created - {response.Content.Created}");
        }
    }
}