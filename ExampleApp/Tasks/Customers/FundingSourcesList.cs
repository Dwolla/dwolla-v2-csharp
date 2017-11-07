using System;
using System.Threading.Tasks;

namespace ExampleApp.Tasks.Customers
{
    [Task("lcfs", "List a Customer's Funding Sources")]
    class FundingSourcesList : BaseTask
    {
        public override async Task Run()
        {
            Write("Customer ID for whom to list the funding sources: ");
            var input = ReadLine();

            var rootRes = await Broker.GetRootAsync();
            var res = await Broker.GetCustomerFundingSourcesAsync(
                new Uri($"{rootRes.Links["customers"].Href}/{input}"));
            res.Embedded.FundingSources
                .ForEach(fs => WriteLine($" - ID:{fs.Id}  Name:{fs.Name} Type:{fs.Type}"));
        }
    }
}