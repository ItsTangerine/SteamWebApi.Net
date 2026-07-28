using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.Converters;

/// <summary>
/// Serializes an enum to/from the exact wire string declared via <see cref="EnumMemberAttribute"/> on each member,
/// instead of the .NET member name. Used for steamwebapi.com enums whose values don't map cleanly to PascalCase
/// (e.g. "priceAz", "sniper rifle", values containing punctuation).
/// Unrecognized incoming values deserialize to the enum's zero value instead of throwing, so a single unexpected
/// value from the API cannot fail deserialization of an entire response.
/// </summary>
/// <typeparam name="TEnum">The enum type to convert.</typeparam>
public sealed class JsonStringEnumMemberConverter<TEnum> : JsonConverter<TEnum>
    where TEnum : struct, Enum
{
    private readonly Dictionary<string, TEnum> _stringToEnum = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<TEnum, string> _enumToString = new();

    /// <summary>Builds the wire-value lookup tables from <see cref="EnumMemberAttribute"/> metadata.</summary>
    public JsonStringEnumMemberConverter()
    {
        foreach (var field in typeof(TEnum).GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            var enumValue = (TEnum)field.GetValue(null)!;
            var wireValue = field.GetCustomAttribute<EnumMemberAttribute>()?.Value ?? field.Name;
            _stringToEnum[wireValue] = enumValue;
            if (!_enumToString.ContainsKey(enumValue))
                _enumToString[enumValue] = wireValue;
        }
    }

    /// <inheritdoc />
    public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var raw = reader.GetString();
        if (raw is not null && _stringToEnum.TryGetValue(raw, out var value))
            return value;
        return default;
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(_enumToString.TryGetValue(value, out var raw) ? raw : value.ToString());
    }

    /// <summary>Converts an enum member to its wire string representation (e.g. for building query strings).</summary>
    public string ToWireValue(TEnum value) => _enumToString.TryGetValue(value, out var raw) ? raw : value.ToString();
}
