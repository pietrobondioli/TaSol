namespace Application.Common.Interfaces;

public interface IMqttMessageHandler
{
    Task HandleMessageAsync(string topic, string payload);
}