using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.Labels
{
    [Task("gl", "Get Label")]
    internal class Get : BaseTask
    {
        public override async Task Run()
        {
            Write("Label ID to retrieve: ");
            var input = ReadLine();

            var response = await HttpService.Labels.GetLabelAsync(input);

            WriteLine(response.Error is not null
                ? $"Error retrieving label. {response.Error.Message}."
                : $"Amount: {response.Content?.Amount?.Value ?? 0} {response.Content?.Amount?.Currency ?? "NA"}");
        }
    }
}