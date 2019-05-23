using System;
using System.Threading.Tasks;
using Dwolla.Client.Models;
using Dwolla.Client.Models.Requests;

namespace ExampleApp.Tasks.Labels
{
    [Task("cl", "Create a Label for a Customer")]
    internal class Create : BaseTask
    {
        public override async Task Run()
        {
            Write("Customer ID for whom to create label: ");
            var input = ReadLine();

            Write("Amount to label: ");
            // Read String input and convert to Decimal
            decimal amount = 0;
            try
            {
                amount = Convert.ToDecimal(Console.ReadLine());
            }
            catch (System.OverflowException) {}
            catch (System.FormatException) {}
            catch (System.ArgumentNullException) {}

            var rootRes = await Broker.GetRootAsync();
            var uri = await Broker.CreateLabelAsync(
                new Uri($"{rootRes.Links["customers"].Href}/{input}/labels"), amount);

            if (uri == null) return;

            var owner = await Broker.GetLabelAsync(uri);
            WriteLine($"Created {owner.Id} ");
        }
    }
}