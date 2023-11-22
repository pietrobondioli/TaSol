namespace Application.Common.Interfaces;

public interface IMailService
{
    Task SendAsync(string to, string subject, string body);

    Task SendAsync(string to, string subject, string body, string from);

    Task SendAsync(string to, string subject, string body, string from, string fromName);
}
