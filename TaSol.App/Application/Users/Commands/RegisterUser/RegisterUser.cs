using Domain.Constants;
using Domain.Entities;

namespace Application.Users.Commands.RegisterUser;

public record RegisterUserCommand : IRequest<long>
{
    public string UserName { get; init; }

    public string Password { get; init; }

    public string Email { get; init; }

    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string PhoneNumber { get; init; }
}

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, long>
{
    private readonly IApplicationDbContext _context;
    private readonly ISecurityUtils _securityUtils;

    public RegisterUserCommandHandler(IApplicationDbContext context, ISecurityUtils securityUtils)
    {
        _context = context;
        _securityUtils = securityUtils;
    }

    public async Task<long> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(
            x => x.UserName == request.UserName || x.Email == request.Email, cancellationToken);

        if (existingUser != null)
        {
            throw new ConflictException("User already exists");
        }

        var entity = new User
        {
            UserName = request.UserName,
            PasswordHash = _securityUtils.HashPassword(request.Password),
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
