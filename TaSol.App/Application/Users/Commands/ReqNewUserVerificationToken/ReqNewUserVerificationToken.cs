using Domain.Entities;
using Domain.Events;

namespace Application.Users.Commands.ReqNewUserVerificationToken;

public record ReqNewUserVerificationTokenCommand : IRequest<long>
{
    public string Email { get; init; }
}

public class ReqNewUserVerificationTokenCommandHandler : IRequestHandler<ReqNewUserVerificationTokenCommand, long>
{
    private readonly IApplicationDbContext _context;

    public ReqNewUserVerificationTokenCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(ReqNewUserVerificationTokenCommand request, CancellationToken cancellationToken)
    {
        var userWithEmail = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

        if (userWithEmail == null)
        {
            throw new NotFoundException(nameof(User), request.Email);
        }

        if (userWithEmail.IsVerified)
        {
            throw new ConflictException("Email already verified");
        }

        await InvalidateExistingTokens(request.Email, cancellationToken);

        var entity = UserEmailVerificationToken.Generate(userWithEmail);

        entity.AddDomainEvent(new UserRequestedNewVerificationTokenEvent(userWithEmail, entity));

        _context.UserEmailVerificationTokens.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }

    private async Task InvalidateExistingTokens(string email, CancellationToken cancellationToken)
    {
        var tokens = await _context.UserEmailVerificationTokens.Where(x => x.User.Email == email)
            .ToListAsync(cancellationToken);

        foreach (var token in tokens)
        {
            token.Revoke();
        }
    }
}
