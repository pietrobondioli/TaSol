using System.Text.Json;
using System.Text.RegularExpressions;
using Infrastructure.Common;
using Serilog.Enrichers.Sensitive;

namespace Infrastructure.Logging;

public class PropertyMaskingOperator : RegexMaskingOperator
{
    private const string VerySpecificPattern = "VERY_SPECIFIC_PATTERN";

    private const string MaskValue = "***MASKED***";

    private readonly IList<string> _propertiesToMask;

    public PropertyMaskingOperator(IEnumerable<string> propertiesToMask)
        : base(VerySpecificPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled)
    {
        _propertiesToMask = propertiesToMask.Select(p => p.ToLower()).ToList();
    }

    protected override string PreprocessInput(string input)
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var dict = JsonSerializer.Deserialize<Dictionary<string, object>>(input, options);
            if (dict != null)
            {
                MaskDictionaryProperties(dict);
                return JsonSerializer.Serialize(dict);
            }
        }
        catch (JsonException)
        {
            return input;
        }

        return input;
    }

    private void MaskDictionaryProperties(Dictionary<string, object> dict)
    {
        var keys = dict.Keys.ToList();
        foreach (var key in keys)
        {
            var loweredKey = key.ToLower();
            if (_propertiesToMask.Contains(loweredKey))
            {
                dict[key] = MaskValue;
            }
            else if (dict[key] is Dictionary<string, object> nestedDict)
            {
                MaskDictionaryProperties(nestedDict);
            }
            else if (dict[key] is JsonElement element && element.ValueKind == JsonValueKind.Object)
            {
                var objDict = element.ToDictionary();
                MaskDictionaryProperties(objDict);
                dict[key] = objDict;
            }
        }
    }
}