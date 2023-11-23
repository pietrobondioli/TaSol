namespace Shared.Settings;

public class MqttSettings
{
    public string BrokerAddress { get; set; }

    public int BrokerPort { get; set; }

    public string Topic { get; set; }

    public string ClientId { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }
}
