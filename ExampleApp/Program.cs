using System;
using System.Linq;
using System.Threading.Tasks;
using Dwolla.Client;

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
                Console.WriteLine("Set DWOLLA_APP_KEY and DWOLLA_APP_SECRET env vars and restart IDE. Press any key to close...");
                Console.ReadLine();
            }
            else
            {
                bool running = true;
                var broker = new DwollaBroker(DwollaClient.Create(isSandbox: true));

                Task.Run(async () => await broker.SetAuthroizationHeader(key, secret)).Wait();

                while (running)
                {
                    Console.Write("What would you like to do? (Press ? for options): ");
                    var input = Console.ReadLine();

                    switch (input.ToLower().Trim())
                    {
                        case "?":
                            Console.WriteLine(@"Options:
 - Quit (q)
 - Help (?)
 - Create a Customer (cc)
 - See all Business Classifications (bcs)");
                            break;
                        case "quit":
                        case "q":
                        case "exit":
                            running = false;
                            break;

                        case "cc":
                            Task.Run(async () => await CreateCustomer(broker)).Wait();
                            break;
                        case "bcs":
                            Task.Run(async () => await GetBusinessClassifications(broker)).Wait();
                            break;
                    }
                }

                
            }
#if DEBUG
#endif
        }

        private static async Task CreateCustomer(DwollaBroker broker)
        {
            var rootRes = await broker.GetRoot();
            foreach (var kvp in rootRes.Links) Console.WriteLine($"{kvp.Key}: {kvp.Value.Href}");

            var createdCustomerUri = await broker.CreateCustomerAsync(
                rootRes.Links["customers"].Href, "night", "man", $"{RandomString(20)}@example.com");

            var customer = await broker.GetCustomerAsync(createdCustomerUri);
            Console.WriteLine($"Created {customer.FirstName} {customer.LastName} with email={customer.Email}");
        }

        private static async Task GetBusinessClassifications(DwollaBroker broker)
        {
            var bcResponse = await broker.GetBusinessClassificationsAsync();

            foreach (var bc in bcResponse.Embedded.BusinessClassifications)
            {
                foreach (var ic in bc.Embedded.IndustryClassifications)
                {
                    Console.WriteLine($"{bc.Name} - {ic.Name}");
                }
            }
        }

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}