using Application.Common.Models;
using Domain.Entities;

namespace Application.Queries.Queries.GetDevicesWithPagination;

public class DeviceDto
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
            CreateMap<Device, DeviceDto>();
        }
    }
}

public class GetDevicesWithPaginationDto : PaginatedList<DeviceDto>
{
    public GetDevicesWithPaginationDto(IReadOnlyCollection<DeviceDto> items, int count, int pageNumber, int pageSize) : base(items, count, pageNumber, pageSize)
    {
    }
}
