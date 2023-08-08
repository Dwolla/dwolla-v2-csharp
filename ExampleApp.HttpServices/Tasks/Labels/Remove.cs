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

            WriteLine($"Label Deleted: {response.Response.Headers.Location.ToString().Split('/').Last()}");
        }
    }
}