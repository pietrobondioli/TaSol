using System.Text.Json;

namespace Infrastructure.Common;

public static class JsonElementExtensions
{
    public static Dictionary<string, object> ToDictionary(this JsonElement element)
    {
        var value = element.Clone();
        var json = value.GetRawText();

        return JsonSerializer.Deserialize<Dictionary<string, object>>(json);
    }
}