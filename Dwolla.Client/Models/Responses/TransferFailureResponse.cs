namespace Dwolla.Client.Models.Responses
{
    public class TransferFailureResponse : BaseResponse
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Explanation { get; set; }
    }
}