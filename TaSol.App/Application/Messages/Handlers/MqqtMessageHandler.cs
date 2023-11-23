using System.Text.Json;
using Application.EnvironmentInfos.Commands.CreateEnvironmentInfo;

namespace Application.Messages.Handlers;

public class MqttMessageHandler : IMqttMessageHandler
{
    private readonly IMediator _mediator;

    public MqttMessageHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task HandleMessageAsync(string topic, string payload)
    {
        var parsedPayload = JsonSerializer.Deserialize<CreateEnvironmentInfoCommand>(payload);

        _mediator.Send(parsedPayload);
        
        return Task.CompletedTask;
    }
}