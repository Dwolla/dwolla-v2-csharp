using System;
using System.Linq;
using System.Threading.Tasks;
using Dwolla.Client;
using static System.Console;
using Dwolla.Client.Models.Responses;

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
 - Get Customers (gc)
 - Create a Customer (cc)
 - Get a Customer's Funding Sources (gcfs)
 - Get Webhook Subscriptions (gws)
 - Create a Webhook Subscription (cws)
 - Delete a Webhook Subscription (dws)
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

                        case "gc":
                            Task.Run(async () => await GetCustomers(broker)).Wait();
                            break;
                        case "cc":
                            Task.Run(async () => await CreateCustomer(broker)).Wait();
                            break;
                        case "gcfs":
                            Task.Run(async () => await GetCustomerFundingSources(broker)).Wait();
                            break;

                        case "gbc":
                            Task.Run(async () => await GetBusinessClassifications(broker)).Wait();
                            break;

                        case "gws":
                            Task.Run(async () => await GetWebhookSubscriptions(broker)).Wait();
                            break;
                        case "cws":
                            Task.Run(async () => await CreateWebhookSubscription(broker)).Wait();
                            break;
                        case "dws":
                            Task.Run(async () => await DeleteWebhookSubscription(broker)).Wait();
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
        
        #region Customers

        private static async Task GetCustomers(DwollaBroker broker)
        {
            var rootRes = await broker.GetRootAsync();
            var res = await broker.GetCustomersAsync(rootRes.Links["customers"].Href);
            res.Embedded.Customers
                .ForEach(c => WriteLine($" - ID:{c.Id}  {c.FirstName} {c.LastName}"));
        }

        private static async Task CreateCustomer(DwollaBroker broker)
        {
            var rootRes = await broker.GetRootAsync();
            var createdCustomerUri = await broker.CreateCustomerAsync(
                rootRes.Links["customers"].Href, "night", "man", $"{RandomString(20)}@example.com");

            var customer = await broker.GetCustomerAsync(createdCustomerUri);
            WriteLine($"Created {customer.FirstName} {customer.LastName} with email={customer.Email}");
        }

        private static async Task GetCustomerFundingSources(DwollaBroker broker)
        {
            Write("Please enter the customer ID for whom you would like to list the funding sources: ");
            var input = ReadLine();

            var rootRes = await broker.GetRootAsync();
            var res = await broker.GetCustomerFundingSourcesAsync(new Uri($"{rootRes.Links["customers"].Href}/{input}"));
            res.Embedded.FundingSources
                .ForEach(fs => WriteLine($" - ID:{fs.Id}  {fs.Name}"));
        }

        #endregion

        #region Webhook Subscriptions

        private static async Task GetWebhookSubscriptions(DwollaBroker broker)
        {
            var rootRes = await broker.GetRootAsync();
            var res = await broker.GetWebhookSubscriptionsAsync(rootRes.Links["webhook-subscriptions"].Href);
            res.Embedded.WebhookSubscriptions
                .ForEach(ws => WriteLine($" - {ws.Id}: {ws.Url}{(ws.Paused ? " PAUSED" : null)}"));
        }

        private static async Task<WebhookSubscription> CreateWebhookSubscription(DwollaBroker broker)
        {
            var rootRes = await broker.GetRootAsync();
            var createdSubscriptionUri = await broker.CreateWebhookSubscriptionAsync(
                rootRes.Links["webhook-subscriptions"].Href, $"http://example.com/webhooks/{RandomString(10)}", RandomString(10));

            var subscription = await broker.GetWebhookSubscriptionAsync(createdSubscriptionUri);
            WriteLine($"Created Subscription {subscription.Id} with url={subscription.Url}");

            return subscription;
        }

        private static async Task DeleteWebhookSubscription(DwollaBroker broker)
        {
            Write("Please enter the ID of the webhook subscription to delete: ");
            var input = ReadLine();

            var rootRes = await broker.GetRootAsync();
            await broker.DeleteWebhookSubscriptionAsync(new Uri(rootRes.Links["webhook-subscriptions"].Href + "/" + input));
            WriteLine($"Deleted Subscription {input}");
        }

        #endregion

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