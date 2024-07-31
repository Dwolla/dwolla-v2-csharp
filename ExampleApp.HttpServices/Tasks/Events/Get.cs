using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.Events
{
    [Task("ge", "Get Event")]
    internal class Get : BaseTask
    {
        public override async Task Run()
        {
            Write("Event ID for whom to retreive: ");
            var input = ReadLine();

            var response = await HttpService.Events.GetEventAsync(input);

            WriteLine($"Event: Id - {response.Content.Id}: {response.Content.Topic} {response.Content.ResourceId}");
        }
    }
}
