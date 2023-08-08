using System;
using System.Threading.Tasks;

namespace ExampleApp.Tasks.LabelReallocations
{
    [Task("clr", "Create Label Reallocation")]
    internal class Create : BaseTask
    {
        public override async Task Run()
        {
            Write("From Label ID: ");
            var fromLabelId = ReadLine();

            Write("To Label ID: ");
            var toLabelId = ReadLine();

            // Read String input and convert to Decimal
            decimal amount = 0;
            WriteLine("Amount to reallocate: ");
            try
            {
                amount = Convert.ToDecimal(Console.ReadLine());
            }
            catch (System.OverflowException) { }
            catch (System.FormatException) { }
            catch (System.ArgumentNullException) { }

            var uri = await Broker.CreateLabelReallocationAsync(fromLabelId, toLabelId, amount);

            if (uri == null) return;

            var entry = await Broker.GetLabelReallocationAsync(uri);
            WriteLine($"Created: {entry.Created}");
        }
    }
}