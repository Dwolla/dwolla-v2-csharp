using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.BusinessClassifications
{
    [Task("gbc", "Get Business Classification")]
    internal class Get : BaseTask
    {
        public override async Task Run()
        {
            Write("Business Classification ID for which to get a specific Business Classification: ");
            var input = ReadLine();

            var response = await HttpService.BusinessClassification.GetBusinessClassificationAsync(input);

            WriteLine($"Name: {response.Content.Name}");
            WriteLine($"Id: {response.Content.Id}");
        }
    }
}