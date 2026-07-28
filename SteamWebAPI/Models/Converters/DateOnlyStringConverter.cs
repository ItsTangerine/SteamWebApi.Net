using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.Converters;

/// <summary>Reads and writes a date-only value as a <c>yyyy-MM-dd</c> string, as used by several request bodies.</summary>
public sealed class DateOnlyStringConverter : JsonConverter<DateTime?>
{
    private const string Format = "yyyy-MM-dd";

    /// <inheritdoc />
    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
            return null;

        var raw = reader.GetString();
        return DateTime.TryParseExact(raw, Format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var value)
            ? value
            : null;
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        if (value is null)
            writer.WriteNullValue();
        else
            writer.WriteStringValue(value.Value.ToString(Format, CultureInfo.InvariantCulture));
    }
}
