using Domain.Entities;

namespace Application.Queries.Queries.GetDeviceById;

public class GetDeviceByIdDto
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string AuthTokenHash { get; set; }

    public bool IsActive { get; set; }

    public long LocationId { get; set; }

    public Location Location { get; set; }

    private sealed class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Device, GetDeviceByIdDto>();
        }
    }
}
