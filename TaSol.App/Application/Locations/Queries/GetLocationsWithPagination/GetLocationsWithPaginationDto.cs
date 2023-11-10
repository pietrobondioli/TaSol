using Domain.Entities;

namespace Application.Queries.Queries.GetLocationsWithPagination;

public class GetLocationsWithPaginationDto
{
    // DTO properties go here

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, GetLocationsWithPaginationDto>(); // Adjust the source entity as needed
        }
    }
}
