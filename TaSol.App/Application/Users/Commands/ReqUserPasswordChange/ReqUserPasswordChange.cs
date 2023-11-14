using Domain.Entities;
using Domain.Events;

namespace Application.Users.Commands.ReqUserPasswordChange;

public record ReqUserPasswordChangeCommand : IRequest<long>
{
    public string Email { get; init; }
}

public class ReqUserPasswordChangeCommandHandler : IRequestHandler<ReqUserPasswordChangeCommand, long>
{
    private readonly IApplicationDbContext _context;

    public ReqUserPasswordChangeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(ReqUserPasswordChangeCommand request, CancellationToken cancellationToken)
    {
        var userWithEmail = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

        if (userWithEmail == null) throw new NotFoundException(nameof(User), request.Email);

        await InvalidateExistingTokens(request.Email, cancellationToken);

        var entity = new UserPasswordResetToken(userWithEmail);

        entity.AddDomainEvent(new UserRequestedPasswordChangeEvent(userWithEmail, entity));

        _context.UserPasswordResetTokens.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }

    private async Task InvalidateExistingTokens(string email, CancellationToken cancellationToken)
    {
        var tokens = await _context.UserPasswordResetTokens.Where(x => x.User.Email == email).ToListAsync(cancellationToken);

        foreach (var token in tokens)
        {
            token.Revoke();
        }
    }
}
