using Domain.Events;
using Microsoft.Extensions.Logging;

namespace Application.Users.EventHandlers;

public class UserChangedPasswordEventHandler : INotificationHandler<UserChangedPasswordEvent>
{
    private readonly ILogger<UserChangedPasswordEventHandler> _logger;

    public UserChangedPasswordEventHandler(ILogger<UserChangedPasswordEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(UserChangedPasswordEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
