using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Commands.Commands.CreateDevice;

public record CreateDeviceCommand : IRequest<long>
{
    // Properties go here
}

public class CreateDeviceCommandHandler : IRequestHandler<CreateDeviceCommand, long>
{
    private readonly IApplicationDbContext _context;

    public CreateDeviceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(CreateDeviceCommand request, CancellationToken cancellationToken)
    {
        // Handler logic goes here
        throw new NotImplementedException(); // Replace with actual return
    }
}
