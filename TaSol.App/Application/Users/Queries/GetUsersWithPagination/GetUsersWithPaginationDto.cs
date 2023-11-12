using Domain.Entities;

namespace Application.Queries.Queries.GetUsersWithPagination;

public class GetUsersWithPaginationDto
{
    // DTO properties go here

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, GetUsersWithPaginationDto>(); // Adjust the source entity as needed
        }
    }
}