using Domain.Events;
using Microsoft.Extensions.Logging;

namespace Application.Users.EventHandlers;

public class UserUpdatedEventHandler : INotificationHandler<UserUpdatedEvent>
{
    private readonly ILogger<UserUpdatedEventHandler> _logger;

    public UserUpdatedEventHandler(ILogger<UserUpdatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(UserUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
