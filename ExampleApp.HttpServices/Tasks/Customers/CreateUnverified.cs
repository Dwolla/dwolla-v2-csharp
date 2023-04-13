using Dwolla.Client.Models.Requests;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.Customers
{
    [Task("cuc", "Create an Unverified Customer")]
    internal class CreateUnverified : BaseTask
    {
        public override async Task Run()
        {
            var rootRes = await HttpService.Root.GetAsync();
            var request = new CreateCustomerRequest
            {
                FirstName = "Night",
                LastName = $"man-{RandomAlphaNumericString(5)}",
                Email = $"{RandomAlphaNumericString(20)}@example.com"
            };
            var idempotencyKey = Guid.NewGuid().ToString();

            var response = await HttpService.Customers.CreateAsync(rootRes.Content.Links["customers"].Href, request, idempotencyKey, default);

            if (response.Response?.Headers?.Location == null) return;
            var getResponse = await HttpService.Customers.GetAsync(response.Response.Headers.Location);
            var customer = getResponse.Content;

            WriteLine($"Created {customer.FirstName} {customer.LastName} with email={customer.Email}");
        }
    }
}