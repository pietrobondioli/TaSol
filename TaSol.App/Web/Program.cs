using Application;
using Infrastructure;
using Serilog;
using Shared.Settings;
using Web;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console());

// Add services to the container.

var configuration = builder.Configuration;
var env = builder.Environment;

// Configure settings DI
var apiSettings = configuration.GetSection("ApiSettings");
builder.Services.Configure<ApiSettings>(apiSettings);
var connectionStrings = configuration.GetSection("ConnectionStrings");
builder.Services.Configure<ConnectionStrings>(connectionStrings);
var jwtSettings = configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSettings);

builder.Services.AddInfrastructureServices(configuration, env, connectionStrings.Get<ConnectionStrings>());
builder.Services.AddApplicationServices();
builder.Services.AddWebServices();

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHealthChecks("/health");

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

app.MapControllers();

#if (UseApiOnly)
app.Map("/", () => Results.Redirect("/api"));
#endif

app.Run();

public partial class Program { }
