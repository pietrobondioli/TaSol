using Application;
using Infrastructure;
using Serilog;
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

builder.Services.AddConfiguration(configuration);
builder.Services.AddInfrastructureServices(configuration, env);
builder.Services.AddApplicationServices();
builder.Services.AddWebServices(configuration);

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(options => { });

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

public partial class Program
{
}
