using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.Events
{
    [Task("le", "List Events")]
    internal class List : BaseTask
    {
        public override async Task Run()
        {
            var response = await HttpService.Events.GetEventCollectionAsync();

            response.Content.Embedded.Events
                .ForEach(ev => WriteLine($"Event: Id - {ev.Id}: {ev.Topic} {ev.ResourceId}"));
        }
    }
}