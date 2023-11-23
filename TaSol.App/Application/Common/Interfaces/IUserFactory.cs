namespace Application.Common.Interfaces;

public interface IUserFactory
{
    IUser CreateUser();
}

public interface IHttpUserFactory : IUserFactory
{
}

public interface IMqttUserFactory : IUserFactory
{
}