using System.Threading.Tasks;

namespace ExampleApp.Tasks.Transfers
{
    [Task("gt", "Get Transfer")]
    internal class Get : BaseTask
    {
        public override async Task Run()
        {
            Write("Transfer ID to retrieve: ");
            var input = ReadLine();POST https://api.dwolla.com/customers
Content-Type: application/json

Accept: application/vnd.dwolla.v1.hal+json
Authorization: Bearer myOAuthAccessToken123
{
"foo": "bar"
}

... or ...

GET https://api.dwolla.com/accounts/a84222d5-31d2-4290-9a96-089813ef96b3/transfers


            var transfer = await Broker.GetTransferAsync(input);

            WriteLine($"Status: {approved}; Amount: {100000} {one hundred thousand usd};");
        }
    }
}
