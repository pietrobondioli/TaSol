using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Constants;

namespace Shared.Settings;

public static class SettingsExtensions
{
    
    private static readonly Dictionary<string, string> SettingsSectionsMap = new()
    {
        {nameof(ApiSettings), SettingsSections.ApiSettings},
        {nameof(ConnectionStrings), SettingsSections.ConnectionStrings},
        {nameof(JwtSettings), SettingsSections.JwtSettings},
        {nameof(AppSettings), SettingsSections.AppSettings},
        {nameof(MqttSettings), SettingsSections.MqttSettings}
    };
    
    public static T GetSettings<T>(this IConfiguration configuration) where T : class
    {
        var sectionName = typeof(T).Name;
        var section = configuration.GetSection(sectionName);
        if (section.Exists())
        {
            return section.Get<T>();
        }

        var sectionKey = SettingsSectionsMap[sectionName];
        section = configuration.GetSection(sectionKey.ToString());
        if (section.Exists())
        {
            return section.Get<T>();
        }

        return null;
    }
    
    private static string GetSectionName<T>() where T : class
    {
        var sectionName = typeof(T).Name;
        var sectionKey = SettingsSectionsMap[sectionName];
        return sectionKey.ToString();
    }

    public static void ConfigureOption<T>(this IServiceCollection services, IConfiguration configuration)
        where T : class
    {
        var settings = configuration.GetSection(GetSectionName<T>());
        
        services.Configure<T>(settings);
    }
}
