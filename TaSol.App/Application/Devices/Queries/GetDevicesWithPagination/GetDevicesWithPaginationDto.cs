using Domain.Entities;

namespace Application.Queries.Queries.GetDevicesWithPagination;

public class GetDevicesWithPaginationDto
{
    // DTO properties go here

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, GetDevicesWithPaginationDto>(); // Adjust the source entity as needed
        }
    }
}