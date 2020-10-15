using System.Threading.Tasks;

namespace ExampleApp.Tasks.Labels
{
    [Task("gl", "Get Label")]
    internal class Get : BaseTask
    {
        public override async Task Run()
        {
            Write("Label ID to retrieve: ");
            var input = ReadLine();

            var label = await Broker.GetLabelAsync(input);

            WriteLine($"Amount: {label.Amount.Value} {label.Amount.Currency}");
        }
    }
}