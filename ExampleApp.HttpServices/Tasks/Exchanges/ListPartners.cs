using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.Exchanges
{
    [Task("lexp", "List Exchange Partners")]
    internal class ListPartners : BaseTask
    {
        public override async Task Run()
        {
            var response = await HttpService.Exchanges.GetPartnerCollectionAsync();

            response.Content.Embedded.ExchangePartners
                .ForEach(ep => WriteLine($"Exchange Partner: ID - {ep.Id} Name - {ep.Name} Status - {ep.Status} Created - {ep.Created}"));
        }
    }
}