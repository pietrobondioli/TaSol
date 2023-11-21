using Domain.Events;
using Microsoft.Extensions.Logging;

namespace Application.Locations.EventHandlers;

public class LocationCreatedEventHandler : INotificationHandler<LocationCreatedEvent>
{
    private readonly ILogger<LocationCreatedEventHandler> _logger;

    public LocationCreatedEventHandler(ILogger<LocationCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(LocationCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
