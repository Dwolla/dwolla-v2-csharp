using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.BeneficialOwners
{
    [Task("rbo", "Remove Beneficial Owner")]
    internal class Remove : BaseTask
    {
        public override async Task Run()
        {
            Write("Beneficial Owner ID to remove: ");
            var input = ReadLine();

            await HttpService.BeneficialOwners.DeleteBeneficialOwnerAsync(input);

            WriteLine("Removed");
        }
    }
}