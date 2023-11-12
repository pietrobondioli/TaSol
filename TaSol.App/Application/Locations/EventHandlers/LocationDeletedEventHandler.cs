using Domain.Events;
using Microsoft.Extensions.Logging;

namespace Application.Locations.EventHandlers;

public class LocationDeletedEventHandler : INotificationHandler<LocationDeletedEvent>
{
    private readonly ILogger<LocationDeletedEventHandler> _logger;

    public LocationDeletedEventHandler(ILogger<LocationDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(LocationDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}