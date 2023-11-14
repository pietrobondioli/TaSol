using Domain.Entities;

namespace Application.Users.Commands.UpdateUser;

public record UpdateUserCommand : IRequest<long>
{
    public long Id { get; init; }

    public string FirstName { get; init; } = string.Empty;

    public string LastName { get; init; } = string.Empty;

    public string PhoneNumber { get; init; } = string.Empty;
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, long>
{
    private readonly IApplicationDbContext _context;

    private readonly IUser _user;

    public UpdateUserCommandHandler(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _user = user;
    }

    public async Task<long> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (user == null) throw new NotFoundException(nameof(User), request.Id);

        if (user.Id != _user.Id) throw new ForbiddenAccessException();

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.PhoneNumber = request.PhoneNumber;

        await _context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
