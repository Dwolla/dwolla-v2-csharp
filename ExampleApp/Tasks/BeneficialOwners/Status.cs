using System;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleApp.Tasks.BeneficialOwners
{
    [Task("gbos", "Get Beneficial Ownership Status")]
    class Status : BaseTask
    {
        public override async Task Run()
        {
            Write("Customer ID for whom to get the status: ");
            var input = ReadLine();

            var rootRes = await Broker.GetRootAsync();
            var statusResponse =
                await Broker.GetBeneficialOwnershipAsync(
                    new Uri($"{rootRes.Links["customers"].Href}/{input}/beneficial-ownership"));

            WriteLine($"Status={statusResponse.Status}");
        }
    }
}