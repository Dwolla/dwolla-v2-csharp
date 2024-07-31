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

            WriteLine(response.Error is not null
                ? $"Error retrieving label reallocation. {response.Error.Message}."
                : $"Label reallocation retrieved. Created: {response.Content.Created}");
        }
    }
}