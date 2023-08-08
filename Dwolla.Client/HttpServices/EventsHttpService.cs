using Dwolla.Client.HttpServices.Architecture;
using Dwolla.Client.Models.Responses;
using Dwolla.Client.Rest;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dwolla.Client.HttpServices
{
    public class EventsHttpService : BaseHttpService
    {
        public EventsHttpService(IDwollaClient dwollaClient, Func<Task<string>> getAccessToken)
           : base(dwollaClient, getAccessToken)
        {
        }

        public async Task<RestResponse<EventResponse>> GetEventAsync(string eventId, CancellationToken cancellation = default)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new ArgumentException("EventId should not be blank.");
            }

            return await GetAsync<EventResponse>(new Uri($"{client.ApiBaseAddress}/events/{eventId}"), cancellation);
        }

        public async Task<RestResponse<GetEventsResponse>> GetEventCollectionAsync(CancellationToken cancellation = default)
        {
            return await GetAsync<GetEventsResponse>(new Uri($"{client.ApiBaseAddress}/events"), cancellation);
        }
    }
}
