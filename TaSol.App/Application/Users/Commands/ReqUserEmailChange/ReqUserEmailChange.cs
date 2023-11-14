using Domain.Entities;
using Domain.Events;

namespace Application.Users.Commands.ReqUserEmailChange;

public record ReqUserEmailChangeCommand : IRequest<long>
{
    public string Email { get; init; }
}

public class ReqUserEmailChangeCommandHandler : IRequestHandler<ReqUserEmailChangeCommand, long>
{
    private readonly IApplicationDbContext _context;

    public ReqUserEmailChangeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(ReqUserEmailChangeCommand request, CancellationToken cancellationToken)
    {
        var userWithEmail = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

        if (userWithEmail == null) throw new NotFoundException(nameof(User), request.Email);

        if (userWithEmail.IsVerified) throw new ConflictException("Email already verified");

        await InvalidateExistingTokens(request.Email, cancellationToken);

        var entity = new UserEmailResetToken(userWithEmail);

        entity.AddDomainEvent(new UserRequestedEmailChangeEvent(userWithEmail, entity));

        _context.UserEmailResetTokens.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }

    private async Task InvalidateExistingTokens(string email, CancellationToken cancellationToken)
    {
        var tokens = await _context.UserEmailResetTokens.Where(x => x.User.Email == email).ToListAsync(cancellationToken);

        foreach (var token in tokens)
        {
            token.Revoke();
        }
    }
}
