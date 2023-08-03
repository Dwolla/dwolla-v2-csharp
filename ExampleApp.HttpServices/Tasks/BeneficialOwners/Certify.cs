using Dwolla.Client.Models.Requests;
using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.BeneficialOwners
{
    [Task("crtbo", "Certify Beneficial Ownership")]
    internal class Certify : BaseTask
    {
        public override async Task Run()
        {
            Write("Customer ID for whom to certify beneficial ownership: ");
            var input = ReadLine();

            var response = await HttpService.BeneficialOwners.CertifyBeneficialOwnerAsync(input, new CertifyBeneficialOwnershipRequest { Status = "certified" });

            if (response == null) return;

            WriteLine("Certified");
        }
    }
}