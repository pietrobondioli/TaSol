using Domain.Events;
using Microsoft.Extensions.Logging;

namespace Application.Devices.EventHandlers;

public class DeviceDeletedEventHandler : INotificationHandler<DeviceDeletedEvent>
{
    private readonly ILogger<DeviceDeletedEventHandler> _logger;

    public DeviceDeletedEventHandler(ILogger<DeviceDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(DeviceDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}