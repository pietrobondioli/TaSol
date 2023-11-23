using System.Text.Json;
using Application.EnvironmentInfos.Commands.CreateEnvironmentInfo;

namespace Application.Messages.Handlers;

public class MqttMessageHandler : IMqttMessageHandler
{
    private readonly ISender _sender;

    public MqttMessageHandler(ISender sender)
    {
        _sender = sender;
    }

    public async Task HandleMessageAsync(string topic, string payload)
    {
        try
        {
            var parsedPayload = JsonSerializer.Deserialize<CreateEnvironmentInfoCommand>(payload);

            var command = new CreateEnvironmentInfoCommand
            {
                AuthToken = parsedPayload.AuthToken,
                Humidity = parsedPayload.Humidity,
                LightLevel = parsedPayload.LightLevel,
                RainLevel = parsedPayload.RainLevel,
                Temperature = parsedPayload.Temperature
            };

            var res = await _sender.Send(command);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
