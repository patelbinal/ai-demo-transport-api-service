using AiDemoTransport.Models.Events;
using AiDemoTransport.Services.Messaging;

namespace AiDemoTransport.Services;

public interface ITransportEventService
{
    Task PublishTransportCreatedAsync(Models.Transport transport);
}

public class TransportEventService : ITransportEventService
{
    private readonly IMessagePublisher _messagePublisher;
    private readonly ILogger<TransportEventService> _logger;

    public TransportEventService(IMessagePublisher messagePublisher, ILogger<TransportEventService> logger)
    {
        _messagePublisher = messagePublisher;
        _logger = logger;
    }

    public async Task PublishTransportCreatedAsync(Models.Transport transport)
    {
        try
        {
            var transportCreatedEvent = new TransportCreatedEvent
            {
                TransportId = transport.Id,
                CarrierId = transport.CarrierId,
                PurchaseId = transport.PurchaseId,
                OfferId = transport.OfferId,
                PickupLocationDetails = transport.PickupLocationDetails,
                DeliveryLocationDetails = transport.DeliveryLocationDetails,
                ScheduleDate = transport.ScheduleDate,
                Status = transport.Status,
                EventId = Guid.NewGuid().ToString(),
                EventTimestamp = DateTime.UtcNow
            };

            // Also publish to a specific queue for direct consumption
            await _messagePublisher.PublishAsync(
                transportCreatedEvent,
                "search.events"
            );

            _logger.LogInformation("Successfully published TransportCreated event for transport ID: {TransportId}", transport.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to publish TransportCreated event for transport ID: {TransportId}", transport.Id);
            throw;
        }
    }
}