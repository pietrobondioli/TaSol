using Domain.Entities;

namespace Application.Devices.Queries.GetDeviceById;

public class GetDeviceByIdDto
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string AuthTokenHash { get; set; }

    public bool IsActive { get; set; }

    public long LocationId { get; set; }

    public LocationDto Location { get; set; }

    private sealed class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Device, GetDeviceByIdDto>();
        }
    }
}

public class LocationDto
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string Country { get; set; }

    public string Latitude { get; set; }

    public string Longitude { get; set; }

    private sealed class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Location, LocationDto>();
        }
    }
}
