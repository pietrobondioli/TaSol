using System.Text;
using Application;
using Domain.Settings;
using Infrastructure;
using Microsoft.OpenApi.Models;

namespace Web;

public class Startup
{
    private const string ApiSettingsSection = "ApiSettings";

    private const string ConnectionStringsSection = "ConnectionStrings";

    private const string JwtSettingsSection = "JwtSettings";

    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        Configuration = configuration;
        Env = env;
    }

    private IConfiguration Configuration { get; }

    private IWebHostEnvironment Env { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // Configure settings DI
        var apiSettings = Configuration.GetSection(ApiSettingsSection);
        services.Configure<ApiSettings>(apiSettings);
        var connectionStrings = Configuration.GetSection(ConnectionStringsSection);
        services.Configure<ConnectionStrings>(connectionStrings);
        var jwtSettings = Configuration.GetSection(JwtSettingsSection);
        services.Configure<JwtSettings>(jwtSettings);

        services.AddInfrastructureServices(Configuration, Env, connectionStrings.Get<ConnectionStrings>());
        services.AddApplicationServices();

        services.AddHttpContextAccessor();

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

        services.AddEndpointsApiExplorer();

        ConfigureSwagger(services);

        services.AddRouting(options => options.LowercaseUrls = true);

        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseHealthChecks("/health");

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.Use(async (context, next) =>
        {
            context.Request.EnableBuffering();
            await next();
        });

        app.UseCors("AllowAllOrigins");

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }

    private void ConfigureSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Trial API", Version = "v1"
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
    }
}