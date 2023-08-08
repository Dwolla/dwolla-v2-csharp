using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.Exchanges
{
    [Task("lex", "List Exchanges")]
    internal class List : BaseTask
    {
        public override async Task Run()
        {
            Write("Customer/Account ID for whom to list the exchanges: ");
            var input = ReadLine();

            var response = await HttpService.Exchanges.GetExchangeCollectionAsync();

            response.Content.Embedded.Exchanges
                .ForEach(ex => WriteLine($"Exchange: ID - {ex.Id} Status - {ex.Status} Create - {ex.Created}"));
        }
    }
}