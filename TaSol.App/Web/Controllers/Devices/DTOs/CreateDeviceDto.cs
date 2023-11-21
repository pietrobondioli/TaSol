namespace Web.Controllers.Devices.DTOs;

public record CreateDeviceDto
{
    public string Name { get; init; } = null!;

    public string? Description { get; init; }

    public long LocationId { get; init; }
}
