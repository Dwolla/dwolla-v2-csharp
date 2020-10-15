using System.Threading.Tasks;

namespace ExampleApp.Tasks.LabelReallocations
{
    [Task("glr", "Get Label Reallocation")]
    internal class Get : BaseTask
    {
        public override async Task Run()
        {
            Write("Label Reallocation ID to retrieve: ");
            var input = ReadLine();

            var labelReallocation = await Broker.GetLabelReallocationAsync(input);

            WriteLine($"Created: {labelReallocation.Created}");
        }
    }
}