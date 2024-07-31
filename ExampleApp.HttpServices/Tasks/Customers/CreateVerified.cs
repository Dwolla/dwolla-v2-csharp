using Dwolla.Client.Models.Requests;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.Customers;

[Task("cvc", "Create a verified Customer")]
internal class CreateVerified : BaseTask
{
    public override async Task Run()
    {
        var request = new CreateCustomerRequest
        {
            FirstName = "Day",
            LastName = $"man-{RandomAlphaNumericString(5)}",
            Email = $"{RandomAlphaNumericString(20)}@example.com",
            DateOfBirth = new DateTime(1980, 1, 1),
            Type = "personal",
            Address1 = "123 easy st",
            City = "Anywhere",
            State = "NY",
            PostalCode = "12345",
            Ssn = "123-45-6789",
            
        };
        var idempotencyKey = Guid.NewGuid().ToString();

        var response = await HttpService.Customers.CreateCustomerAsync(request, idempotencyKey, default);

        if (response.Response?.Headers?.Location == null) return;

        var getResponse = await HttpService.Customers.GetCustomerAsync(response.Response.Headers.Location.ToString().Split('/').Last());
        var customer = getResponse.Content;

        WriteLine($"Created: {customer.FirstName} {customer.LastName} {customer.Email} {customer.Id}");
    }
}