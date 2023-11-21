using Domain.Events;
using Microsoft.Extensions.Logging;

namespace Application.Users.EventHandlers;

public class UserRequestedPasswordChangeEventHandler : INotificationHandler<UserRequestedPasswordChangeEvent>
{
    private readonly ILogger<UserRequestedPasswordChangeEventHandler> _logger;

    public UserRequestedPasswordChangeEventHandler(ILogger<UserRequestedPasswordChangeEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(UserRequestedPasswordChangeEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
