using System;
using System.Threading.Tasks;

namespace ExampleApp.Tasks.Documents
{
    [Task("ld", "List Documents")]
    class List : BaseTask
    {
        public override async Task Run()
        {
            Write("Customer ID for whom to upload a document: ");
            var input = ReadLine();

            var rootRes = await Broker.GetRootAsync();
            var res = await Broker.GetCustomerDocumentsAsync(new Uri($"{rootRes.Links["customers"].Href}/{input}"));
            res.Embedded.Documents
                .ForEach(d => WriteLine($" - ID:{d.Id}  {d.Type} {d.Status}"));
        }
    }
}