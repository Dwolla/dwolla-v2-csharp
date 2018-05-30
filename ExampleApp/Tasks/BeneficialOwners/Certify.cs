using System;
using System.Threading.Tasks;

namespace ExampleApp.Tasks.BeneficialOwners
{
    [Task("crtbo", "Certify Beneficial Ownership")]
    internal class Certify : BaseTask
    {
        public override async Task Run()
        {
            Write("Customer ID for whom to certify beneficial ownership: ");
            var input = ReadLine();

            var rootRes = await Broker.GetRootAsync();
            var uri = await Broker.CertifyBeneficialOwnershipAsync(
                new Uri($"{rootRes.Links["customers"].Href}/{input}/beneficial-ownership"));

            if (uri == null) return;

            WriteLine("Certified");
        }
    }
}