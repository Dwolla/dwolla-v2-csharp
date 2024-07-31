using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.Documents
{
    [Task("ld", "List Documents")]
    internal class List : BaseTask
    {
        public override async Task Run()
        {
            Write("Customer ID for whom to list the documents: ");
            var input = ReadLine();

            var response = await HttpService.Documents.GetDocumentCollectionAsync(input);

            response.Content.Embedded.Documents
                .ForEach(d => WriteLine($"Document: ID - {d.Id} | Type - {d.Type} | Status - {d.Status}"));
        }
    }
}