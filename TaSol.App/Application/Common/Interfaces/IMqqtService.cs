namespace Application.Common.Interfaces;

public interface IMqttService
{
    Task ConnectAsync();

    Task SubscribeAsync(string topic);

    Task PublishAsync(string topic, string payload);

    Task UnsubscribeAsync(string topic);

    Task DisconnectAsync();
}
