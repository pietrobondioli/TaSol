using Application.Common.Models;
using Domain.Entities;

namespace Application.Queries.Queries.GetLocationsWithPagination;

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

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Location, LocationDto>();
        }
    }
}

public class GetLocationsWithPaginationDto : PaginatedList<LocationDto>
{
    public GetLocationsWithPaginationDto(IReadOnlyCollection<LocationDto> items, int count, int pageNumber, int pageSize) : base(items, count, pageNumber, pageSize)
    {
    }
}
