using System.Text.Json;
using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.Converters;

/// <summary>Reads a Unix timestamp expressed in whole seconds into a <see cref="DateTimeOffset"/>.</summary>
public sealed class UnixSecondsConverter : JsonConverter<DateTimeOffset?>
{
    /// <inheritdoc />
    public override DateTimeOffset? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.Null:
                return null;
            case JsonTokenType.Number:
                return DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64());
            case JsonTokenType.String when long.TryParse(reader.GetString(), out var seconds):
                return DateTimeOffset.FromUnixTimeSeconds(seconds);
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
            writer.WriteNumberValue(value.Value.ToUnixTimeSeconds());
    }
}
