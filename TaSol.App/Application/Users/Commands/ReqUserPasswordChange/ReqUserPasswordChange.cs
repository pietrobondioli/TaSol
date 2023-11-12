namespace Application.Users.Commands.ReqUserPasswordChange;

public record ReqUserPasswordChangeCommand : IRequest<long>
{
    // Properties go here
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
        // Handler logic goes here
        throw new NotImplementedException(); // Replace with actual return
    }
}