using Application.Common.Interfaces;

namespace Infrastructure.Services;

public class MqttUser : IUser
{
    public long? Id { get; } = null;
}
