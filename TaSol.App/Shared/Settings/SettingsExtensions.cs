using Microsoft.Extensions.Configuration;
using Shared.Constants;

namespace Shared.Settings;

public static class SettingsExtensions
{
    public static ApiSettings GetApiSettings(this IConfiguration configuration)
    {
        var apiSettings = configuration.GetSection(SettingsSections.ApiSettings);
        
        return apiSettings.Get<ApiSettings>();
    }
    
    public static ConnectionStrings GetConnectionStrings(this IConfiguration configuration)
    {
        var connectionStrings = configuration.GetSection(SettingsSections.ConnectionStrings);
        
        return connectionStrings.Get<ConnectionStrings>();
    }
    
    public static JwtSettings GetJwtSettings(this IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection(SettingsSections.JwtSettings);
        
        return jwtSettings.Get<JwtSettings>();
    }
    
    public static AppSettings GetAppSettings(this IConfiguration configuration)
    {
        var appSettings = configuration.GetSection(SettingsSections.AppSettings);
        
        return appSettings.Get<AppSettings>();
    }
    
    public static MqttSettings GetMqttSettings(this IConfiguration configuration)
    {
        var mqttSettings = configuration.GetSection(SettingsSections.MqttSettings);
        
        return mqttSettings.Get<MqttSettings>();
    }
}
