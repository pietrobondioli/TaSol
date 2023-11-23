using Application.Common.Interfaces;

namespace Web.Common.Services;

public class HttpUserFactory : IHttpUserFactory
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpUserFactory(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public IUser CreateUser()
    {
        // Assuming CurrentUser is your IUser implementation for HTTP context
        // return new CurrentUser(_httpContextAccessor);
        return new HttpUser(_httpContextAccessor);
    }
}
