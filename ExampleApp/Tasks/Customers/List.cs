using System.Threading.Tasks;

namespace ExampleApp.Tasks.Customers
{
    [Task("lc", "List Customers")]
    internal class List : BaseTask
    {
        public override async Task Run()
        {
            var rootRes = await Broker.GetRootAsync();
            var res = await Broker.GetCustomersAsync(rootRes.Links["customers"].Href);
            res.Embedded.Customers
                .ForEach(c => WriteLine($" - ID:{c.Id}  {c.FirstName} {c.LastName}"));
        }
    }
}