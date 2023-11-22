using System.Text;
using Application.Common.Interfaces;
using Infrastructure.Data;
using Infrastructure.Utils;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Shared.Constants;
using Shared.Settings;
using Web.Common.Interfaces;
using Web.Common.Services;
using Web.Configuration.Filters;

namespace Web;

public static class DependencyInjection
{
    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var apiSettings = configuration.GetSection(SettingsSections.ApiSettings);
        services.Configure<ApiSettings>(apiSettings);

        var connectionStrings = configuration.GetSection(SettingsSections.ConnectionStrings);
        services.Configure<ConnectionStrings>(connectionStrings);

        var jwtSettings = configuration.GetSection(SettingsSections.JwtSettings);
        services.Configure<JwtSettings>(jwtSettings);

        return services;
    }

    public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigureDIs(services);

        ConfigureJwtAuthentication(services, configuration);

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
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "TaSol API", Version = "v1" });

            c.AddSecurityDefinition("StaticApiKey",
                new OpenApiSecurityScheme
                {
                    Description = "Static API Key passed in the X-Static-Api-Key header",
                    In = ParameterLocation.Header,
                    Name = "X-Static-Api-Key",
                    Type = SecuritySchemeType.ApiKey
                });

            c.AddSecurityDefinition("JwtBearer",
                new OpenApiSecurityScheme
                {
                    Description =
                        "API Jwt Authorization header using the JwtKey scheme. Example: \"Authorization: Bearer {api_key}\"",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                }
            );

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
            
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "JwtBearer" }
                    },
                    new List<string>()
                }
            });

            c.UseInlineDefinitionsForEnums();
            c.SchemaFilter<EnumSchemaFilter>();
        });

        services.AddRouting(o =>
        {
            o.LowercaseUrls = true;
        });

        services.AddControllers();

        services.AddRazorPages();

        services.AddEndpointsApiExplorer();

        return services;
    }

    private static void ConfigureDIs(IServiceCollection services)
    {
        services.AddScoped<IUser, CurrentUser>();

        services.AddSingleton<ISecurityUtils, SecurityUtils>();

        services.AddSingleton<IJwtService, JwtService>();
    }

    private static void ConfigureJwtAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection(SettingsSections.JwtSettings).Get<JwtSettings>();

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            })
            .AddJwtBearer("JwtBearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = creds.Key,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
    }
}
