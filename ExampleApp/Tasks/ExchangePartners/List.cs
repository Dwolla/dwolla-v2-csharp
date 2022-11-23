using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ExampleApp.Tasks.ExchangePartners
{
    [Task("lexp", "List Events")]
    internal class List : BaseTask
    {
        public override async Task Run()
        {
            var res = await Broker.GetExchangePartnersAsync();
            res.Embedded.ExchangePartners
                .ForEach(ep => WriteLine($" - ID: {ep.Id} Name: {ep.Name} Status: {ep.Status} Created: {ep.Created}"));
        }
    }
}