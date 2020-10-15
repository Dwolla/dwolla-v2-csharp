using System;
using System.Threading.Tasks;

namespace ExampleApp.Tasks.Documents
{
    [Task("ld", "List Documents")]
    internal class List : BaseTask
    {
        public override async Task Run()
        {
            Write("Customer ID for whom to list the documents: ");
            var input = ReadLine();

            var rootRes = await Broker.GetRootAsync();
            var res = await Broker.GetCustomerDocumentsAsync(new Uri($"{rootRes.Links["customers"].Href}/{input}"));
            res.Embedded.Documents
                .ForEach(d => WriteLine($" - ID: {d.Id}  {d.Type} {d.Status}"));
        }
    }
}