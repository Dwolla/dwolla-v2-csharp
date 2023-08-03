using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.Labels
{
    [Task("glr", "Get Label Reallocation")]
    internal class GetLabelReallocation : BaseTask
    {
        public override async Task Run()
        {
            Write("Label Reallocation ID to retrieve: ");
            var input = ReadLine();

            var response = await HttpService.Labels.GetReallocationAsync(input);

            WriteLine($"Created: {response.Content.Created}");
        }
    }
}