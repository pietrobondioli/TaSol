using Domain.Entities;

namespace Application.Queries.Queries.GetLocationById;

public class GetLocationByIdDto
{
    // DTO properties go here

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, GetLocationByIdDto>(); // Adjust the source entity as needed
        }
    }
}
