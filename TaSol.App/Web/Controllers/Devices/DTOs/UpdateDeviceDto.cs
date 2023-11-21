namespace Web.Controllers.Devices.DTOs;

public record UpdateDeviceDto
{
    public string Name { get; init; }

    public string Description { get; init; }

    public long LocationId { get; init; }
}
