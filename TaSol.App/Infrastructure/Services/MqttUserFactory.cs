using Application.Common.Interfaces;

namespace Infrastructure.Services;

public class MqttUserFactory : IMqttUserFactory
{
    public IUser CreateUser()
    {
        return new MqttUser();
    }
}
