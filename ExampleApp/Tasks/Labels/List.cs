using System;
using System.Threading.Tasks;

namespace ExampleApp.Tasks.Labels
{
    [Task("ll", "List Labels for a Customer")]
    internal class List : BaseTask
    {
        public override async Task Run()
        {
            Write("Customer ID for whom to list the labels: ");
            var input = ReadLine();

            var res = await Broker.GetLabelsAsync(input);
            res.Embedded.Labels
                .ForEach(l => WriteLine($" - ID: {l.Id}; Amount: {l.Amount.Value} {l.Amount.Currency}"));
        }
    }
}