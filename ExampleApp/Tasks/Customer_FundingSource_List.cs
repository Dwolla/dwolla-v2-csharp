using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExampleApp.Tasks
{
    [Task("lcfs", "List a Customer's Funding Sources")]
    class Customer_FundingSource_List : BaseTask
    {
        public override async Task Run()
        {
            Write("Please enter the customer ID for whom you would like to list the funding sources: ");
            var input = ReadLine();

            var rootRes = await Broker.GetRootAsync();
            var res = await Broker.GetCustomerFundingSourcesAsync(new Uri($"{rootRes.Links["customers"].Href}/{input}"));
            res.Embedded.FundingSources
                .ForEach(fs => WriteLine($" - ID:{fs.Id}  Name:{fs.Name} Type:{fs.Type}"));
        }
    }
}
