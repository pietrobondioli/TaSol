using Domain.Entities;

namespace Application.Queries.Queries.GetDeviceById;

public class GetDeviceByIdDto
{
    // DTO properties go here

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, GetDeviceByIdDto>(); // Adjust the source entity as needed
        }
    }
}