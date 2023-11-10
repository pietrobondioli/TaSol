using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Commands.Commands.DeleteDevice;

public record DeleteDeviceCommand : IRequest<long>
{
    // Properties go here
}

public class DeleteDeviceCommandHandler : IRequestHandler<DeleteDeviceCommand, long>
{
    private readonly IApplicationDbContext _context;

    public DeleteDeviceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(DeleteDeviceCommand request, CancellationToken cancellationToken)
    {
        // Handler logic goes here
        throw new NotImplementedException(); // Replace with actual return
    }
}
