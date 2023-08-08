using Dwolla.Client.Models;
using Dwolla.Client.Models.Requests;
using System;
using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.BeneficialOwners
{
    [Task("cbo", "Create Beneficial Owner")]
    internal class Create : BaseTask
    {
        public override async Task Run()
        {
            Write("Customer ID for whom to create a beneficial owner: ");
            var input = ReadLine();

            var uri = await HttpService.BeneficialOwners.CreateBeneficialOwnerAsync(
                input,
                new CreateBeneficialOwnerRequest
                {
                    FirstName = $"Testing Beneficial {RandomNumericString(5)}",
                    LastName = $"Testing Owner {RandomNumericString(5)}",
                    Ssn = "123-45-6789",
                    DateOfBirth = new DateTime(2023, 5, 28),
                    Address = new Address
                    {
                        Address1 = "Street",
                        City = "City",
                        StateProvinceRegion = "TX",
                        Country = "US",
                        PostalCode = "78631"
                    }
                });

            if (uri == null) return;

            var owner = await HttpService.BeneficialOwners.GetBeneficialOwnerCollectionAsync(input);

            owner.Content.Embedded.BeneficialOwners.ForEach(bo => WriteLine($"Created: {bo.FirstName} - {bo.LastName}"));
        }
    }
}