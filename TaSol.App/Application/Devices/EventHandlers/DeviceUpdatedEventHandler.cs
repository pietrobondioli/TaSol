using Domain.Events;
using Microsoft.Extensions.Logging;

namespace Application.Devices.EventHandlers;

public class DeviceUpdatedEventHandler : INotificationHandler<DeviceUpdatedEvent>
{
    private readonly ILogger<DeviceUpdatedEventHandler> _logger;

    public DeviceUpdatedEventHandler(ILogger<DeviceUpdatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(DeviceUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}