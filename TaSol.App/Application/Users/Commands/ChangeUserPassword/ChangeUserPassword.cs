using Domain.Entities;
using Domain.Events;

namespace Application.Users.Commands.ChangeUserPassword;

public record ChangeUserPasswordCommand : IRequest<long>
{
    public string Token { get; init; }

    public string NewPassword { get; init; }

    public string ConfirmNewPassword { get; init; }
}

public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, long>
{
    private readonly IApplicationDbContext _context;

    private readonly ISecurityUtils _securityUtils;

    public ChangeUserPasswordCommandHandler(IApplicationDbContext context, ISecurityUtils securityUtils)
    {
        _context = context;
        _securityUtils = securityUtils;
    }

    public async Task<long> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var token = await _context.UserPasswordResetTokens.FirstOrDefaultAsync(x => x.Token == request.Token,
            cancellationToken);

        if (token == null)
        {
            throw new NotFoundException(nameof(UserPasswordResetToken), request.Token);
        }

        if (!token.IsActive)
        {
            throw new ConflictException("Invalid token");
        }

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == token.UserId, cancellationToken);

        if (user == null)
        {
            throw new NotFoundException(nameof(User), token.UserId);
        }

        user.PasswordHash = _securityUtils.HashPassword(request.NewPassword);
        user.AddDomainEvent(new UserChangedPasswordEvent(user));

        token.Consume();

        await _context.SaveChangesAsync(cancellationToken);

        return token.Id;
    }
}
