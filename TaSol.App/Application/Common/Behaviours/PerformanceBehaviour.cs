using System.Diagnostics;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviours;

public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;
    private readonly IUser _user;
    private readonly IApplicationDbContext _context;

    public PerformanceBehaviour(
        ILogger<TRequest> logger,
        IUser user,
        IApplicationDbContext context)
    {
        _timer = new Stopwatch();
        _logger = logger;
        _user = user;
        _context = context;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds > 500)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _user.Id ?? default;
            var userName = string.Empty;

            if (!userId.Equals(default))
            {
                userName = await _context.Users
                    .Where(u => u.Id == userId)
                    .Select(u => u.UserName)
                    .FirstOrDefaultAsync(cancellationToken);
            }

            _logger.LogWarning("CleanArchitecture Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
                requestName, elapsedMilliseconds, userId, userName, request);
        }

        return response;
    }
}
