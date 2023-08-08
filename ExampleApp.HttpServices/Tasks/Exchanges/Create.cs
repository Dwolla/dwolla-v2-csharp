using Dwolla.Client.Models;
using Dwolla.Client.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.Exchanges
{
    [Task("cex", "Create Exchange for a Customer")]
    internal class Create : BaseTask
    {
        public override async Task Run()
        {
            Write("Customer ID for whom to create an exchange: ");
            var input = ReadLine();
            Write("MX Token: ");
            var token = ReadLine();

            var createResponse = await HttpService.Exchanges.CreateExchangeCustomerAsync(
                input,
                new CreateExchangeRequest
                {
                    Token = token,
                    Links = new Dictionary<string, Link>
                    {
                        {"exchange-partner", new Link {Href = new Uri("https://api-sandbox.dwolla.com/exchange-partners/bca8d065-49a5-475b-a6b4-509bc8504d22")}}
                    },
                });

            if (createResponse == null) return;

            var response = await HttpService.Exchanges.GetExchangeAsync(createResponse.Response.Headers.Location.ToString().Split('/').Last());
            WriteLine($"Created Exchange ID: {response.Content.Id} - Created: {response.Content.Created}");
        }
    }
}