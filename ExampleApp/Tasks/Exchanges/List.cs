using System;
using System.Threading.Tasks;

namespace ExampleApp.Tasks.Exchanges
{
    [Task("lex", "List Exchanges")]
    internal class List : BaseTask
    {
        public override async Task Run()
        {
            Write("Customer/Account ID for whom to list the exchanges: ");
            var input = ReadLine();

            var rootRes = await Broker.GetRootAsync();
            var res = await Broker.GetExchangesAsync(new Uri($"{rootRes.Links["customers"].Href}/{input}/exchanges"));
            res.Embedded.Exchanges
                .ForEach(ex => WriteLine($" - ID: {ex.Id} Status: {ex.Status} Created: {ex.Created}"));
        }
    }
}