using Microsoft.AspNetCore.Http;

namespace Application.Common.Services;

public class ContextAwareUserFactory : IUserFactory
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHttpUserFactory _httpUserFactory;
    private readonly IMqttUserFactory _mqttUserFactory;

    public ContextAwareUserFactory(
        IHttpContextAccessor httpContextAccessor, 
        IHttpUserFactory httpUserFactory, 
        IMqttUserFactory mqttUserFactory)
    {
        _httpContextAccessor = httpContextAccessor;
        _httpUserFactory = httpUserFactory;
        _mqttUserFactory = mqttUserFactory;
    }

    public IUser CreateUser()
    {
        if (_httpContextAccessor.HttpContext != null)
        {
            return _httpUserFactory.CreateUser();
        }
        else
        {
            return _mqttUserFactory.CreateUser();
        }
    }
}
