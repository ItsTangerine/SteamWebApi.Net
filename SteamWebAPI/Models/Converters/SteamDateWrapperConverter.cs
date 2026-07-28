using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.Converters;

/// <summary>
/// Reads the PHP-style date wrapper steamwebapi.com uses for price timestamps
/// (<c>{ "date": "yyyy-MM-dd HH:mm:ss.ffffff", "timezone_type": 3, "timezone": "UTC" }</c>) into a
/// <see cref="DateTimeOffset"/>. Also accepts a plain ISO 8601 string or a JSON null, since some endpoints
/// emit either form for otherwise-equivalent fields.
/// </summary>
public sealed class SteamDateWrapperConverter : JsonConverter<DateTimeOffset?>
{
    private const string WrapperDateFormat = "yyyy-MM-dd HH:mm:ss.ffffff";

    /// <inheritdoc />
    public override DateTimeOffset? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.Null:
                return null;

            case JsonTokenType.String:
            {
                var raw = reader.GetString();
                return string.IsNullOrEmpty(raw) ? null : DateTimeOffset.Parse(raw!, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
            }

            case JsonTokenType.StartObject:
            {
                using var doc = JsonDocument.ParseValue(ref reader);
                if (!doc.RootElement.TryGetProperty("date", out var dateProp))
                    return null;

                var dateStr = dateProp.GetString();
                if (string.IsNullOrEmpty(dateStr))
                    return null;

                var parsed = DateTime.ParseExact(dateStr!, WrapperDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);
                return new DateTimeOffset(parsed, TimeSpan.Zero);
            }

            default:
                reader.Skip();
                return null;
        }
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, DateTimeOffset? value, JsonSerializerOptions options)
    {
        if (value is null)
            writer.WriteNullValue();
        else
            writer.WriteStringValue(value.Value);
    }
}
