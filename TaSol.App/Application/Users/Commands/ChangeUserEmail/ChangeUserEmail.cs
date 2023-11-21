using Domain.Entities;
using Domain.Events;

namespace Application.Users.Commands.ChangeUserEmail;

public record ChangeUserEmailCommand : IRequest<long>
{
    public string Token { get; init; }

    public string NewEmail { get; init; }
}

public class ChangeUserEmailCommandHandler : IRequestHandler<ChangeUserEmailCommand, long>
{
    private readonly IApplicationDbContext _context;

    public ChangeUserEmailCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(ChangeUserEmailCommand request, CancellationToken cancellationToken)
    {
        var token = await _context.UserEmailResetTokens.FirstOrDefaultAsync(x => x.Token == request.Token,
            cancellationToken);

        if (token == null)
        {
            throw new NotFoundException(nameof(UserEmailResetToken), request.Token);
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

        user.Email = request.NewEmail;
        user.AddDomainEvent(new UserChangedEmailEvent(user));

        token.Consume();

        await _context.SaveChangesAsync(cancellationToken);

        return token.Id;
    }
}
