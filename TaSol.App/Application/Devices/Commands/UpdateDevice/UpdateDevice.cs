using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Commands.Commands.UpdateDevice;

public record UpdateDeviceCommand : IRequest<long>
{
    // Properties go here
}

public class UpdateDeviceCommandHandler : IRequestHandler<UpdateDeviceCommand, long>
{
    private readonly IApplicationDbContext _context;

    public UpdateDeviceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
    {
        // Handler logic goes here
        return default; // Replace with actual return
    }
}
