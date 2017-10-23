using System;
using System.Linq;
using System.Threading.Tasks;
using Dwolla.Client;
using static System.Console;

namespace ExampleApp
{
    public class Program
    {
        private static readonly Random Random = new Random();

        static void Main()
        {
            var key = Environment.GetEnvironmentVariable("DWOLLA_APP_KEY");
            var secret = Environment.GetEnvironmentVariable("DWOLLA_APP_SECRET");

            if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(secret))
            {
                WriteLine("Set DWOLLA_APP_KEY and DWOLLA_APP_SECRET env vars and restart IDE. Press any key to exit..");
                ReadLine();
            }
            else
            {
                var running = true;
                var broker = new DwollaBroker(DwollaClient.Create(isSandbox: true));

                Task.Run(async () => await broker.SetAuthroizationHeader(key, secret)).Wait();

                while (running)
                {
                    Write("What would you like to do? (Press ? for options): ");
                    var input = ReadLine();

                    switch (input.ToLower().Trim())
                    {
                        case "?":
                            WriteLine(@"Options:
 - Quit (q)
 - Help (?)
 - Get root (gr)
 - Create a Customer (cc)
 - Get Business Classifications (gbc)");
                            break;
                        case "quit":
                        case "q":
                        case "exit":
                            running = false;
                            break;
                        case "gr":
                            Task.Run(async () => await GetRoot(broker)).Wait();
                            break;
                        case "cc":
                            Task.Run(async () => await CreateCustomer(broker)).Wait();
                            break;
                        case "gbc":
                            Task.Run(async () => await GetBusinessClassifications(broker)).Wait();
                            break;
                    }
                }
            }
        }

        private static async Task GetRoot(DwollaBroker broker)
        {
            var res = await broker.GetRootAsync();
            foreach (var kvp in res.Links) WriteLine($"{kvp.Key}: {kvp.Value.Href}");
        }

        private static async Task CreateCustomer(DwollaBroker broker)
        {
            var rootRes = await broker.GetRootAsync();
            var createdCustomerUri = await broker.CreateCustomerAsync(
                rootRes.Links["customers"].Href, "night", "man", $"{RandomString(20)}@example.com");

            var customer = await broker.GetCustomerAsync(createdCustomerUri);
            WriteLine($"Created {customer.FirstName} {customer.LastName} with email={customer.Email}");
        }

        private static async Task GetBusinessClassifications(DwollaBroker broker)
        {
            var res = await broker.GetBusinessClassificationsAsync();
            res.Embedded.BusinessClassifications
                .ForEach(bc => bc.Embedded.IndustryClassifications
                    .ForEach(ic => WriteLine($"{bc.Name} - {ic.Name}")));
        }

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}