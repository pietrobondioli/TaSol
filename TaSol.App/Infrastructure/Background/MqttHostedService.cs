using Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Shared.Settings;

namespace Infrastructure.Background;

public class MqttHostedService : IHostedService
{
    private readonly IMqttService _mqttService;
    private readonly MqttSettings _mqttSettings;

    public MqttHostedService(IMqttService mqttService, IOptions<MqttSettings> mqttSettings)
    {
        _mqttService = mqttService;
        _mqttSettings = mqttSettings.Value;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _mqttService.ConnectAsync();
        await _mqttService.SubscribeAsync(_mqttSettings.Topic);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
