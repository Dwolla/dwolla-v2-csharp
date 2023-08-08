using Dwolla.Client.Models;
using Dwolla.Client.Models.Requests;
using System.Reflection;
using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.Documents
{
    [Task("cd", "Create Document")]
    internal class Create : BaseTask
    {
        private const string FilenameSuccess = "test-document-upload-success.png";

        public override async Task Run()
        {
            Write("Customer ID for whom to upload a document: ");
            var input = ReadLine();

            using var fileStream = typeof(Create).GetTypeInfo().Assembly
                .GetManifestResourceStream($"ExampleApp.HttpServices.{FilenameSuccess}");

            var response = await HttpService.Documents.UploadDocumentAsync(
                input,
                new UploadDocumentRequest
                {
                    DocumentType = "idCard",
                    Document = new File
                    {
                        ContentType = "image/png",
                        Filename = FilenameSuccess,
                        Stream = fileStream
                    }
                });

            if (response == null) return;

            WriteLine($"Customer document uploaded: URI - {response.Response.Headers.Location}");
        }
    }
}