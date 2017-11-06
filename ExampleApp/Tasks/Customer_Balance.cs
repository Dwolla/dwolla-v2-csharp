using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleApp.Tasks
{
    [Task("gcb", "Get Customer Balance")]
    class Customer_Balance : BaseTask
    {
        public override async Task Run()
        {
            Write("Please enter the customer ID for whom you would like to retrieve the balance: ");
            var input = ReadLine();

            var rootRes = await Broker.GetRootAsync();
            var sourcesRes = await Broker.GetCustomerFundingSourcesAsync(new Uri($"{rootRes.Links["customers"].Href}/{input}"));
            var balanceRes = await Broker.GetFundingSourceBalance(sourcesRes.Embedded.FundingSources.First(x => x.Type == "balance").Links["balance"].Href);

            WriteLine($" Balance: {balanceRes.Balance.Value} {balanceRes.Balance.Currency}");
        }
    }
}
