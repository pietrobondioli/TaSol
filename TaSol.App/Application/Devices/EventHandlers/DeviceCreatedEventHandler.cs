using Domain.Events;
using Microsoft.Extensions.Logging;

namespace Application.Devices.EventHandlers;

public class DeviceCreatedEventHandler : INotificationHandler<DeviceCreatedEvent>
{
    private readonly ILogger<DeviceCreatedEventHandler> _logger;

    public DeviceCreatedEventHandler(ILogger<DeviceCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(DeviceCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
