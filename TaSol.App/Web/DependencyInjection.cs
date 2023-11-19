using Application.Common.Interfaces;
using Infrastructure.Data;
using Infrastructure.Utils;
using Microsoft.OpenApi.Models;
using Web.Common.Middleware;
using Web.Services;

namespace Web;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        ConfigureDIs(services);

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddExceptionHandler<CustomExceptionHandler>();

        services.AddSwaggerGen();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "TaSol API",
                Version = "v1"
            });

            c.AddSecurityDefinition("StaticApiKey",
                new OpenApiSecurityScheme
                {
                    Description = "Static API Key passed in the X-Static-Api-Key header",
                    In = ParameterLocation.Header,
                    Name = "X-Static-Api-Key",
                    Type = SecuritySchemeType.ApiKey
                });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme, Id = "StaticApiKey"
                        }
                    },
                    new List<string>()
                }
            });
        });

        services.AddRouting(options => options.LowercaseUrls = true);

        services.AddControllers();

        services.AddRazorPages();

        services.AddEndpointsApiExplorer();

        return services;
    }

    private static void ConfigureDIs(IServiceCollection services)
    {
        services.AddScoped<IUser, CurrentUser>();

        services.AddSingleton<ISecurityUtils, SecurityUtils>();
    }
}