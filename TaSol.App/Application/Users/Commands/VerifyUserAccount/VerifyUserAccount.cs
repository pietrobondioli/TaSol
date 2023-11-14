using Domain.Entities;
using Domain.Events;

namespace Application.Users.Commands.VerifyUserAccount;

public record VerifyUserAccountCommand : IRequest<long>
{
    public string Token { get; init; } = string.Empty;
}

public class VerifyUserAccountCommandHandler : IRequestHandler<VerifyUserAccountCommand, long>
{
    private readonly IApplicationDbContext _context;

    public VerifyUserAccountCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(VerifyUserAccountCommand request, CancellationToken cancellationToken)
    {
        var token = await _context.UserEmailVerificationTokens.FirstOrDefaultAsync(x => x.Token == request.Token, cancellationToken);

        if (token == null) throw new NotFoundException(nameof(UserEmailVerificationToken), request.Token);

        if (!token.IsActive) throw new ConflictException("Invalid token");

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == token.UserId, cancellationToken);

        if (user == null) throw new NotFoundException(nameof(User), token.UserId);

        user.IsVerified = true;
        user.AddDomainEvent(new UserVerifiedAccountEvent(user));

        token.Consume();

        await _context.SaveChangesAsync(cancellationToken);

        return token.Id;
    }
}
