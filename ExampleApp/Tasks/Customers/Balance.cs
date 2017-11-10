using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleApp.Tasks.Customers
{
    [Task("gcb", "Get Customer Balance")]
    class Balance : BaseTask
    {
        public override async Task Run()
        {
            Write("Customer ID for whom to get the balance: ");
            var input = ReadLine();

            var rootRes = await Broker.GetRootAsync();
            var sourcesRes = await Broker.GetCustomerFundingSourcesAsync(new Uri($"{rootRes.Links["customers"].Href}/{input}"));
            var balanceRes = await Broker.GetFundingSourceBalanceAsync(sourcesRes.Embedded.FundingSources.First(x => x.Type == "balance").Links["balance"].Href);

            WriteLine($" Balance: {balanceRes.Balance.Value} {balanceRes.Balance.Currency}");
        }
    }
}
