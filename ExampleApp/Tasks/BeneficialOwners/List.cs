using System;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleApp.Tasks.BeneficialOwners
{
    [Task("lbo", "List Beneficial Owners")]
    class List : BaseTask
    {
        public override async Task Run()
        {
            Write("Customer ID for whom to list the beneficial owners: ");
            var input = ReadLine();

            var rootRes = await Broker.GetRootAsync();
            var listResponse = await Broker.GetBeneficialOwnersAsync(new Uri($"{rootRes.Links["customers"].Href}/{input}/beneficial-owners"));

            WriteLine($"{listResponse.Embedded.BeneficialOwners.Count()} beneficial owners retrieved:");

            foreach (var x in listResponse.Embedded.BeneficialOwners)
            {
                WriteLine($"{x.FirstName} {x.LastName} ({x.Id})");
            }
        }
    }
}