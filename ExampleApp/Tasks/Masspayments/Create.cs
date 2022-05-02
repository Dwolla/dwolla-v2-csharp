using System;
using System.Threading.Tasks;

namespace ExampleApp.Tasks.Masspayments
{
    [Task("cmp", "Create Masspayment")]
    internal class Create : BaseTask
    {
        public override async Task Run()
        {
            Write("Funding Source ID from which to transfer: ");
            var sourceFundingSourceId = ReadLine();

            Write("Destination Funding Source ID for first masspay item: ");
            var destinationFundingSourceId1 = ReadLine();

            Write("Destination Funding Source ID for second masspay item: ");
            var destinationFundingSourceId2 = ReadLine();

            var amount1 = decimal.Parse("1.11");
            var amount2 = decimal.Parse("2.22");

            Uri uri;

            uri = await Broker.CreateMasspaymentAsync(sourceFundingSourceId, destinationFundingSourceId1, amount1, destinationFundingSourceId2, amount2);

            if (uri == null) return;

            var masspayment = await Broker.GetMasspaymentAsync(uri);
            WriteLine($"Created {masspayment.Id}; Status: {masspayment.Status}; Total: {masspayment.Total.Value} {masspayment.Total.Currency}; Created: {masspayment.Created}");
        }
    }
}