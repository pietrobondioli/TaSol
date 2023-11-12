namespace Application.EnvironmentInfos.Commands.CreateStaticEnvironmentInfo;

public record CreateStaticEnvironmentInfoCommand : IRequest<long>
{
    // Properties go here
}

public class CreateStaticEnvironmentInfoCommandHandler : IRequestHandler<CreateStaticEnvironmentInfoCommand, long>
{
    private readonly IApplicationDbContext _context;

    public CreateStaticEnvironmentInfoCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(CreateStaticEnvironmentInfoCommand request, CancellationToken cancellationToken)
    {
        // Handler logic goes here
        throw new NotImplementedException(); // Replace with actual return
    }
}