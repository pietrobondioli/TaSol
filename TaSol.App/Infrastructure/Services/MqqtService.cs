using System.Text;
using Application.Common.Interfaces;
using MQTTnet;
using MQTTnet.Client;
using Shared.Settings;

namespace Infrastructure.Services;

public class MqttService : IMqttService
{
    private readonly IMqttClient _mqttClient;
    private readonly MqttSettings _mqttSettings;

    public MqttService(MqttSettings mqttSettings, IMqttMessageHandler mqttMessageHandler)
    {
        var factory = new MqttFactory();
        _mqttClient = factory.CreateMqttClient();
        _mqttSettings = mqttSettings;
        
        _mqttClient.ApplicationMessageReceivedAsync += async (e) =>
        {
            await mqttMessageHandler.HandleMessageAsync(e.ApplicationMessage.Topic, string.Empty);
        };
    }

    public async Task ConnectAsync()
    {
        var options = new MqttClientOptionsBuilder()
            .WithTcpServer(_mqttSettings.BrokerAddress, _mqttSettings.BrokerPort)
            .WithCredentials(_mqttSettings.Username, _mqttSettings.Password)
            .Build();

        await _mqttClient.ConnectAsync(options);
    }

    public async Task SubscribeAsync(string topic)
    {
        if (!_mqttClient.IsConnected)
            await ConnectAsync();

        var topicFilter = new MqttTopicFilterBuilder()
            .WithTopic(topic)
            .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce)
            .Build();

        await _mqttClient.SubscribeAsync(topicFilter);
    }

    public Task PublishAsync(string topic, string payload)
    {
        if (!_mqttClient.IsConnected)
            throw new InvalidOperationException("Client is not connected");

        var message = new MqttApplicationMessageBuilder()
            .WithTopic(topic)
            .WithPayload(payload)
            .Build();

        return _mqttClient.PublishAsync(message);
    }

    public async Task UnsubscribeAsync(string topic)
    {
        if (!_mqttClient.IsConnected)
            await ConnectAsync();

        await _mqttClient.UnsubscribeAsync(topic);
    }

    public async Task DisconnectAsync()
    {
        if (!_mqttClient.IsConnected)
            await ConnectAsync();

        await _mqttClient.DisconnectAsync();
    }
}
