using Domain.Events;
using Microsoft.Extensions.Logging;

namespace Application.Locations.EventHandlers;

public class LocationUpdatedEventHandler : INotificationHandler<LocationUpdatedEvent>
{
    private readonly ILogger<LocationUpdatedEventHandler> _logger;

    public LocationUpdatedEventHandler(ILogger<LocationUpdatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(LocationUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}