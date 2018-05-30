using System;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleApp.Tasks.BeneficialOwners
{
    [Task("lbo", "List Beneficial Owners")]
    internal class List : BaseTask
    {
        public override async Task Run()
        {
            Write("Customer ID for whom to list the beneficial owners: ");
            var input = ReadLine();

            var rootRes = await Broker.GetRootAsync();
            var listRes = await Broker.GetBeneficialOwnersAsync(
                new Uri($"{rootRes.Links["customers"].Href}/{input}/beneficial-owners"));

            WriteLine($"{listRes.Embedded.BeneficialOwners.Count()} beneficial owners retrieved:");

            foreach (var x in listRes.Embedded.BeneficialOwners) WriteLine($"{x.FirstName} {x.LastName} ({x.Id})");
        }
    }
}