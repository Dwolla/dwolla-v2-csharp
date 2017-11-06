using System.Threading.Tasks;

namespace ExampleApp.Tasks
{
    [Task("lc", "List Customers")]
    class Customer_List : BaseTask
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