using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.BeneficialOwners
{
    [Task("gbos", "Get Beneficial Ownership Status")]
    internal class Status : BaseTask
    {
        public override async Task Run()
        {
            Write("Customer ID for whom to get the status: ");
            var input = ReadLine();

            var response = await HttpService.BeneficialOwners.GetBeneficialOwnershipAsync(input);

            WriteLine($"Status: {response.Content.Status}");
        }
    }
}