using Application.Common.Interfaces;
using Domain.Constants;
using Domain.Entities;

namespace Application.Users.Commands.Register;

public record RegisterCommand : IRequest<long>
{
    public string? UserName { get; init; }

    public string? Password { get; init; }

    public string? Email { get; init; }

    public string? FirstName { get; init; }

    public string? LastName { get; init; }

    public string? PhoneNumber { get; init; }
}

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, long>
{
    private readonly IApplicationDbContext _context;

    public RegisterCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var entity = new User()
        {
            UserName = request.UserName,
            PasswordHash = request.Password,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            Role = Roles.User,
            IsVerified = false,
            Metadata = new UserMetadata()
        };

        _context.Users.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
