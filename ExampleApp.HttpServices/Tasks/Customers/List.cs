
using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.Customers
{
    [Task("lc", "List Customers")]
    internal class List : BaseTask
    {
        public override async Task Run()
        {
            var rootRes = await HttpService.Root.GetAsync();
            var res = await HttpService.Customers.GetCollectionAsync(rootRes.Content.Links["customers"].Href);
            res.Content.Embedded.Customers
                .ForEach(c => WriteLine($" - ID:{c.Id}  {c.FirstName} {c.LastName}"));
        }
    }
}