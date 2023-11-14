using Domain.Entities;

namespace Application.Users.Commands.AuthenticateUser;

public record AuthenticateUserCommand : IRequest<long>
{
    public string UserName { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;
}

public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, long>
{
    private readonly IApplicationDbContext _context;

    private readonly ISecurityUtils _securityUtils;

    public AuthenticateUserCommandHandler(IApplicationDbContext context, ISecurityUtils securityUtils)
    {
        _context = context;
        _securityUtils = securityUtils;
    }

    public async Task<long> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName || x.Email == request.Email, cancellationToken);

        if (user == null) throw new NotFoundException(nameof(User), request.UserName);

        if (!_securityUtils.VerifyPassword(request.Password, user.PasswordHash)) throw new ConflictException("Credentials are invalid");

        return user.Id;
    }
}
