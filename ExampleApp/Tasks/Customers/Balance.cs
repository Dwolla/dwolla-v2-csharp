using System;
using System.Linq;
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
            var sourcesRes =
                await Broker.GetCustomerFundingSourcesAsync(new Uri($"{rootRes.Links["customers"].Href}/{input}"));
            var balanceRes = await Broker.GetFundingSourceBalanceAsync(sourcesRes.Embedded.FundingSources
                .First(x => x.Type == "balance").Links["balance"].Href);

            var balance = balanceRes.Balance;
            WriteLine(balance == null ? $"Status={balanceRes.Status}" : $"Balance={balance.Value} {balance.Currency}");
        }
    }
}