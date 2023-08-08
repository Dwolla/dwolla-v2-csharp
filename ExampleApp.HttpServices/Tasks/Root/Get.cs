using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.Root
{
    [Task("gr", "Get root")]
    internal class Get : BaseTask
    {
        public override async Task Run()
        {
            var response = await HttpService.Root.GetAsync();

            foreach (var kvp in response.Content.Links) WriteLine($"{kvp.Key}: {kvp.Value.Href}");
        }
    }
}