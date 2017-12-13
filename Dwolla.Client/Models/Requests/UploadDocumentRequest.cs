namespace Dwolla.Client.Models.Requests
{
    public class UploadDocumentRequest
    {
        public string DocumentType { get; set; }
        public File Document { get; set; }
    }
}