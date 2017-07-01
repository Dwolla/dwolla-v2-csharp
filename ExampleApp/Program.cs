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
            var broker = new DwollaBroker(DwollaClient.Create(isSandbox: true));
            var key = Environment.GetEnvironmentVariable("DWOLLA_APP_KEY");
            var secret = Environment.GetEnvironmentVariable("DWOLLA_APP_SECRET");

            if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(secret))
            {
                Console.WriteLine("Set DWOLLA_APP_KEY and DWOLLA_APP_SECRET env vars and restart IDE. Exiting...");
                Environment.Exit(1);
            }

            Task.Run(async () =>
            {
                await broker.SetAuthroizationHeader(key, secret);

                var rootRes = await broker.GetRoot();
                foreach (var kvp in rootRes.Links) Console.WriteLine($"{kvp.Key}: {kvp.Value.Href}");

                var createdCustomerUri = await broker.CreateCustomerAsync(
                    rootRes.Links["customers"].Href, "night", "man", $"{RandomString(20)}@example.com");

                var customer = await broker.GetCustomer(createdCustomerUri);
                Console.WriteLine($"Created {customer.FirstName} {customer.LastName} with email={customer.Email}");
            }).GetAwaiter().GetResult();
        }

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}