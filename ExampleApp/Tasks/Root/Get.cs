using System.Threading.Tasks;

namespace ExampleApp.Tasks.Root
{
    [Task("gr", "Get root")]
    class Get : BaseTask
    {
        public override async Task Run()
        {
            var res = await Broker.GetRootAsync();
            foreach (var kvp in res.Links) WriteLine($"{kvp.Key}: {kvp.Value.Href}");
        }
    }
}