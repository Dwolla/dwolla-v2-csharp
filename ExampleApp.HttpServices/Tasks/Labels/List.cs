using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.Labels
{
    [Task("ll", "List Labels for a Customer")]
    internal class List : BaseTask
    {
        public override async Task Run()
        {
            Write("Customer ID for whom to list the labels: ");
            var input = ReadLine();

            var response = await HttpService.Labels.GetLabelCustomerCollectionAsync(input, null, null);

            if (response.Error is not null)
            {
                WriteLine($"Error retrieving label for customer. {response.Error.Message}.");
            }
            else
            {
                response.Content.Embedded.Labels.ForEach(l => WriteLine($"Label: ID - {l.Id}; Amount - {l.Amount.Value} {l.Amount.Currency}"));
            }
        }
    }
}