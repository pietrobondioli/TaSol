using Domain.Entities;

namespace Application.Queries.Queries.GetEnvironmentInfoById;

public class GetEnvironmentInfoByIdDto
{
    // DTO properties go here

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, GetEnvironmentInfoByIdDto>(); // Adjust the source entity as needed
        }
    }
}