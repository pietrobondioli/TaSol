using Application.Common.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Interceptors;
using Infrastructure.Logging;
using Infrastructure.Mail;
using Infrastructure.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Debugging;
using Serilog.Enrichers.Sensitive;
using Serilog.Exceptions;
using Shared.Constants;
using Shared.Settings;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration, IWebHostEnvironment env)
    {
        services.AddSingleton<IMqttService, MqttService>();
        
        services.AddScoped<IMailService, AwsMailService>();

        ConfigureDatabase(services, configuration, env);

        ConfigureSerilog(services, configuration, env);

        return services;
    }

    private static void ConfigureDatabase(IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment env)
    {
        var connectionStrings = configuration.GetSettings<ConnectionStrings>();

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            if (env.IsDevelopment() && string.IsNullOrEmpty(connectionStrings.Database))
            {
                options.UseInMemoryDatabase("ApplicationDbContext");
                return;
            }

            options.UseSqlServer(connectionStrings.Database, sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                sqlOptions.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30), null);
            });
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();
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
                    new PropertyMaskingOperator(new List<string> { "Password", "Token" })
                };
            })
            .CreateLogger());
    }
}
