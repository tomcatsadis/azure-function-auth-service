using AuthService.Application.Repositories;
using AuthService.Domain.Entities;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.EventPublisher
{
    public class EventHubPublisher : IEventPublisher
    {
        private readonly IEventHubPublisherSettings _eventHubPublisherSettings;

        private readonly ILogger<EventHubPublisher> _logger;

        public EventHubPublisher(IEventHubPublisherSettings eventHubPublisherSettings, ILogger<EventHubPublisher> logger)
        {
            _eventHubPublisherSettings = eventHubPublisherSettings;
            _logger = logger;
        }

        public async Task SendNewUserEvent(User user)
        {
            await Send(
                connectionString: _eventHubPublisherSettings.NewUserEventConnectionString,
                eventHubName: _eventHubPublisherSettings.NewUserEventName,
                user);
        }

        public async Task Send<T>(string connectionString, string eventHubName, T data)
        {
            await using var producerClient = new EventHubProducerClient(connectionString, eventHubName);

            using EventDataBatch eventBatch = await producerClient.CreateBatchAsync();

            eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data))));

            await producerClient.SendAsync(eventBatch);
        }
    }
}
