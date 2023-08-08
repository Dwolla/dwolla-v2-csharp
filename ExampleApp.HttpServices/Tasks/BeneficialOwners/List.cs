using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.BeneficialOwners
{
    [Task("lbo", "List Beneficial Owners")]
    internal class List : BaseTask
    {
        public override async Task Run()
        {
            Write("Customer ID for whom to list the beneficial owners: ");
            var input = ReadLine();

            var response = await HttpService.BeneficialOwners.GetBeneficialOwnerCollectionAsync(input);

            WriteLine($"{response.Content.Embedded.BeneficialOwners.Count} Beneficial Owners Retrieved:");

            response.Content.Embedded.BeneficialOwners.ForEach(bo => WriteLine($"{bo.FirstName} {bo.LastName} ({bo.Id})"));
        }
    }
}