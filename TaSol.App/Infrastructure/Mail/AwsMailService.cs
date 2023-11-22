using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Mail;

public class AwsMailService : IMailService
{
    private readonly ILogger<AwsMailService> _logger;

    public AwsMailService(ILogger<AwsMailService> logger)
    {
        _logger = logger;
    }

    public Task SendAsync(string to, string subject, string body)
    {
        _logger.LogInformation("Sending email to {To} with subject {Subject}", to, subject);

        throw new NotImplementedException();
    }

    public Task SendAsync(string to, string subject, string body, string from)
    {
        _logger.LogInformation("Sending email to {To} with subject {Subject} from {From}", to, subject, from);
        
        throw new NotImplementedException();
    }

    public Task SendAsync(string to, string subject, string body, string from, string fromName)
    {
        _logger.LogInformation("Sending email to {To} with subject {Subject} from {From} ({FromName})", to, subject, from, fromName);
        
        throw new NotImplementedException();
    }
}
