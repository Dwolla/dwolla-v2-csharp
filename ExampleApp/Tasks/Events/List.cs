using System.Threading.Tasks;

namespace ExampleApp.Tasks.Events
{
    [Task("le", "List Events")]
    class List : BaseTask
    {
        public override async Task Run()
        {
            var rootRes = await Broker.GetRootAsync();
            var res = await Broker.GetEventsAsync(rootRes.Links["events"].Href);
            res.Embedded.Events
                .ForEach(ev => WriteLine($" - {ev.Id}: {ev.Topic} {ev.ResourceId}"));
        }
    }
}