using System.Threading.Tasks;

namespace ExampleApp.Tasks.Labels
{
    [Task("rl", "Remove Label")]
    internal class Remove : BaseTask
    {
        public override async Task Run()
        {
            Write("Label ID to remove: ");
            var input = ReadLine();

            await Broker.DeleteLabelAsync(input);
        }
    }
}