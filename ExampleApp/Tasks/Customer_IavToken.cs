using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExampleApp.Tasks
{
    [Task("gcit", "Get a Customer IAV Token")]
    class Customer_IavToken : BaseTask
    {
        public override async Task Run()
        {
            Write("Please enter the customer ID for whom you would like to get an IAV token: ");
            var input = ReadLine();

            var rootRes = await Broker.GetRootAsync();
            var res = await Broker.GetCustomerIavToken(new Uri($"{rootRes.Links["customers"].Href}/{input}"));
            WriteLine($"Token created: {res.Token}");
        }
    }
}
