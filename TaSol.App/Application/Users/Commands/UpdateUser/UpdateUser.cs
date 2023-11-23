using Domain.Entities;

namespace Application.Users.Commands.UpdateUser;

public record UpdateUserCommand : IRequest<long>
{
    public long Id { get; init; }

    public string? FirstName { get; init; } = null!;

    public string? LastName { get; init; } = null!;

    public string? PhoneNumber { get; init; } = null!;
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, long>
{
    private readonly IApplicationDbContext _context;

    private readonly IUser _user;

    public UpdateUserCommandHandler(IApplicationDbContext context, IUserFactory userFactory)
    {
        _context = context;
        _user = userFactory.CreateUser();
    }

    public async Task<long> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.Id);
        }

        if (user.Id != _user.Id)
        {
            throw new ForbiddenAccessException();
        }

        if (request.FirstName != null)
        {
            user.FirstName = request.FirstName;
        }

        if (request.LastName != null)
        {
            user.LastName = request.LastName;
        }

        if (request.PhoneNumber != null)
        {
            user.PhoneNumber = request.PhoneNumber;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
