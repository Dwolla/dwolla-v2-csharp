using System.Linq;
using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.Labels
{
    [Task("rl", "Remove Label")]
    internal class Remove : BaseTask
    {
        public override async Task Run()
        {
            Write("Label ID to remove: ");
            var input = ReadLine();

            var response = await HttpService.Labels.DeleteLabelAsync(input);

            if (response.Error is not null)
            {
                WriteLine($"Error removing label. {response.Error.Message}");
            }
            else if (response.Response.Headers?.Location is not null)
            {
                WriteLine($"Label Deleted: {response.Response.Headers.Location.ToString().Split('/').Last()}");
            }
            else
            {
                WriteLine("Label Deleted.");
            }
        }
    }
}