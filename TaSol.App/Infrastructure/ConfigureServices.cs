using System.Reflection;
using Infrastructure.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Debugging;
using Serilog.Enrichers.Sensitive;
using Serilog.Exceptions;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration, IWebHostEnvironment env)
    {
        // Configure Repositories
        services.Scan(scan => scan
            .FromAssemblies(
                Assembly.GetExecutingAssembly()
            )
            .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Repository")))
            .AsImplementedInterfaces()
            .WithTransientLifetime());

        ConfigureSerilog(services, configuration, env);

        return services;
    }

    private static void ConfigureSerilog(IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            SelfLog.Enable(Console.Error);
        }

        services.AddSingleton(new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.WithExceptionDetails()
            .Enrich.WithSensitiveDataMasking(options =>
            {
                options.MaskingOperators = new List<IMaskingOperator>
                {
                    new IbanMaskingOperator(),
                    new CreditCardMaskingOperator(),
                    new PropertyMaskingOperator(new List<string>
                    {
                        "Password", "Token"
                    })
                };
            })
            .CreateLogger());
    }
}
