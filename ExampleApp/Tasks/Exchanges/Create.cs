using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dwolla.Client;
using Dwolla.Client.Models;
using Dwolla.Client.Models.Requests;

namespace ExampleApp.Tasks.Exchanges
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

            var rootRes = await Broker.GetRootAsync();
            var uri = await Broker.CreateExchangeAsync(
                new Uri($"{rootRes.Links["customers"].Href}/{input}/exchanges"), token);

            if (uri == null) return;

            var exchange = await Broker.GetExchangeAsync(uri);
            WriteLine($"Created Exchange ID: {exchange.Id} Created: {exchange.Created}");
        }
    }
}