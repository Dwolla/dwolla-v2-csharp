using System;
using System.Reflection;
using System.Threading.Tasks;
using Dwolla.Client.Models;
using Dwolla.Client.Models.Requests;

namespace ExampleApp.Tasks.Documents
{
    [Task("cd", "Create Document")]
    internal class Create : BaseTask
    {
        private const string FilenameSuccess = "test-document-upload-success.png";

        public override async Task Run()
        {
            Write("Customer ID for whom to upload a document: ");
            var input = ReadLine();

            var rootRes = await Broker.GetRootAsync();

            using (var fileStream = typeof(Create).GetTypeInfo().Assembly
                .GetManifestResourceStream($"ExampleApp.{FilenameSuccess}"))
            {
                var uri = await Broker.UploadDocumentAsync(
                    new Uri($"{rootRes.Links["customers"].Href}/{input}/documents"),
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

                if (uri == null) return;

                WriteLine($"Customer document uploaded: URI={uri}");
            }
        }
    }
}