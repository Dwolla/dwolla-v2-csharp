using System;
using System.Threading.Tasks;
using Dwolla.Client.Models;
using Dwolla.Client.Models.Requests;

namespace ExampleApp.Tasks.BeneficialOwners
{
    [Task("cbo", "Create Beneficial Owner")]
    internal class Create : BaseTask
    {
        public override async Task Run()
        {
            Write("Customer ID for whom to create a beneficial owner: ");
            var input = ReadLine();

            var rootRes = await Broker.GetRootAsync();
            var uri = await Broker.CreateBeneficialOwnerAsync(
                new Uri($"{rootRes.Links["customers"].Href}/{input}/beneficial-owners"),
                new CreateBeneficialOwnerRequest
                {
                    FirstName = "Beneficial",
                    LastName = $"Owner{RandomNumericString(5)}",
                    Ssn = "123-45-6789",
                    DateOfBirth = new DateTime(1970, 1, 1),
                    Address = new Address
                    {
                        Address1 = "Street",
                        City = "City",
                        StateProvinceRegion = "VA",
                        Country = "US",
                        PostalCode = "12345"
                    }
                });

            if (uri == null) return;

            var owner = await Broker.GetBeneficialOwnerAsync(uri);
            WriteLine($"Created {owner.FirstName} {owner.LastName}");
        }
    }
}