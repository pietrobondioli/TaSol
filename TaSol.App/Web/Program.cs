using Application;
using Application.Common.Interfaces;
using Infrastructure;
using Infrastructure.Background;
using Serilog;
using Shared.Settings;
using Web;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console());

var configuration = builder.Configuration;
var env = builder.Environment;

builder.Services.AddConfiguration(configuration);
builder.Services.AddInfrastructureServices(configuration, env);
builder.Services.AddApplicationServices();
builder.Services.AddWebServices(configuration);

builder.Services.AddHostedService<MqttHostedService>();

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

app.Run();

public partial class Program
{
}
