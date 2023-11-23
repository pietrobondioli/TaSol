using System.Text.Json;
using Application.EnvironmentInfos.Commands.CreateEnvironmentInfo;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Messages.Handlers;

public class MqttMessageHandler : IMqttMessageHandler
{
    private readonly IServiceProvider _serviceProvider;

    public MqttMessageHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task HandleMessageAsync(string topic, string payload)
    {
        using var scope = _serviceProvider.CreateScope();
        var sender = scope.ServiceProvider.GetRequiredService<ISender>();
        var parsedPayload = JsonSerializer.Deserialize<CreateEnvironmentInfoCommand>(payload);

        var command = new CreateEnvironmentInfoCommand
        {
            AuthToken = parsedPayload.AuthToken,
            Humidity = parsedPayload.Humidity,
            LightLevel = parsedPayload.LightLevel,
            RainLevel = parsedPayload.RainLevel,
            Temperature = parsedPayload.Temperature
        };

        var res = await sender.Send(command);
    }
}
