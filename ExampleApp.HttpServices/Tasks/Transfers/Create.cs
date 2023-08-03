using Dwolla.Client.Models;
using Dwolla.Client.Models.Requests;
using Dwolla.Client.Models.Responses;
using Dwolla.Client.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.Transfers
{
    [Task("ct", "Create Transfer")]
    internal class Create : BaseTask
    {
        public override async Task Run()
        {
            Write("Funding Source ID from which to transfer: ");
            var sourceFundingSourceId = ReadLine();
            Write("Funding Source ID to which to transfer: ");
            var destinationFundingSourceId = ReadLine();

            Write("Include a fee? (y/n): ");
            var includeFee = ReadLine();

            Write("Include ACH details? (y/n): ");
            var includeAchDetails = ReadLine();

            string sourceAddenda = null;
            string destinationAddenda = null;

            if ("y".Equals(includeAchDetails, StringComparison.CurrentCultureIgnoreCase))
            {
                Write("Enter ACH details for Source bank account: ");
                sourceAddenda = ReadLine();

                Write("Enter ACH details for Destination bank account: ");
                destinationAddenda = ReadLine();
            }

            RestResponse<EmptyResponse> response;

            if ("y".Equals(includeFee, StringComparison.CurrentCultureIgnoreCase))
            {
                var fundingSource = await HttpService.FundingSources.GetFundingSourceAsync(destinationFundingSourceId);

                response = await HttpService.Transfers.CreateTransferAsync(
                    new CreateTransferRequest
                    {
                        Amount = new Money
                        {
                            Currency = "USD",
                            Value = 50
                        },
                        Links = new Dictionary<string, Link>
                        {
                            { "source", new Link { Href = new Uri($"https://api-sandbox.dwolla.com/funding-sources/{sourceFundingSourceId}") } },
                            { "destination", new Link { Href = new Uri($"https://api-sandbox.dwolla.com/funding-sources/{destinationFundingSourceId}") } }
                        },
                        Fees = new List<Fee>
                            {
                                new Fee
                                {
                                    Amount = new Money { Value = 1, Currency = "USD" },
                                    Links = new Dictionary<string, Link> { { "charge-to", new Link { Href = fundingSource.Content.Links["customer"].Href } } }
                                }
                            },
                        AchDetails = sourceAddenda == null || destinationAddenda == null
                            ? null
                            : new AchDetails
                            {
                                Source = new SourceAddenda
                                {
                                    Addenda = new Addenda
                                    {
                                        Values = new List<string> { sourceAddenda }
                                    }
                                },
                                Destination = new DestinationAddenda
                                {
                                    Addenda = new Addenda
                                    {
                                        Values = new List<string> { destinationAddenda }
                                    }
                                }
                            }
                    }
                );
            }
            else
            {
                response = await HttpService.Transfers.CreateTransferAsync(
                    new CreateTransferRequest
                    {
                        Amount = new Money
                        {
                            Currency = "USD",
                            Value = 50
                        },
                        Links = new Dictionary<string, Link>
                        {
                            { "source", new Link { Href = new Uri($"https://api-sandbox.dwolla.com/funding-sources/{sourceFundingSourceId}") } },
                            { "destination", new Link { Href = new Uri($"https://api-sandbox.dwolla.com/funding-sources/{destinationFundingSourceId}") } }
                        },
                        Fees = null,
                        AchDetails = sourceAddenda == null || destinationAddenda == null
                            ? null
                            : new AchDetails
                            {
                                Source = new SourceAddenda
                                {
                                    Addenda = new Addenda
                                    {
                                        Values = new List<string> { sourceAddenda }
                                    }
                                },
                                Destination = new DestinationAddenda
                                {
                                    Addenda = new Addenda
                                    {
                                        Values = new List<string> { destinationAddenda }
                                    }
                                }
                            }
                    }
                );
            }

            if (response == null) return;

            var transferResponse = await HttpService.Transfers.GetTransferAsync(response.Response.Headers.Location.ToString().Split('/').Last());
            WriteLine($"Created {transferResponse.Content.Id}; Status: {transferResponse.Content.Status}");
        }
    }
}