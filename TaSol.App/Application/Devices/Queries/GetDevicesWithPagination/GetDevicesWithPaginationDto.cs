using Application.Common.Models;
using Domain.Entities;

namespace Application.Devices.Queries.GetDevicesWithPagination;

public class GetDevicesWithPaginationDto : PaginatedList<DeviceDto>
{
    public GetDevicesWithPaginationDto(IReadOnlyCollection<DeviceDto> items, int count, int pageNumber, int pageSize) :
        base(items, count, pageNumber, pageSize)
    {
    }
}

public class DeviceDto
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
            CreateMap<Device, DeviceDto>();
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
