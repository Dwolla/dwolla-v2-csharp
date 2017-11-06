using System.Threading.Tasks;

namespace ExampleApp.Tasks
{
    [Task("gr", "Get root")]
    class RootResources_Get : BaseTask
    {
        public override async Task Run()
        {
            var res = await Broker.GetRootAsync();
            foreach (var kvp in res.Links) WriteLine($"{kvp.Key}: {kvp.Value.Href}");
        }
    }
}