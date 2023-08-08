using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.BeneficialOwners
{
    [Task("gbo", "Get Beneficial-Owner")]
    internal class Get : BaseTask
    {
        public override async Task Run()
        {
            Write("Beneficial-Owner ID for which to get a specific Beneficial-Owner: ");
            var input = ReadLine();

            var response = await HttpService.BeneficialOwners.GetBeneficialOwnerAsync(input);

            WriteLine($"FirstName: {response.Content.FirstName}");
            WriteLine($"LastName: {response.Content.LastName}");
            WriteLine($"VerificationStatus: {response.Content.VerificationStatus}");
        }
    }
}