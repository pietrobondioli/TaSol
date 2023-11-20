using Application.Common.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Interceptors;
using Infrastructure.Logging;
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
        var connectionStrings = configuration.GetSection(SettingsSections.ConnectionStrings).Get<ConnectionStrings>();
        
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseSqlServer(connectionStrings.Database);
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        ConfigureSerilog(services, configuration, env);

        return services;
    }

    private static void ConfigureSerilog(IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment env)
    {
        if (env.IsDevelopment()) SelfLog.Enable(Console.Error);

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
