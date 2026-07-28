using System.Text.Json;
using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.Converters;

/// <summary>
/// Reads a boolean value that steamwebapi.com sometimes represents as a JSON <c>true</c>/<c>false</c> literal and
/// sometimes as the integer <c>1</c>/<c>0</c>, depending on the endpoint.
/// </summary>
public sealed class FlexibleBooleanConverter : JsonConverter<bool>
{
    /// <inheritdoc />
    public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        reader.TokenType switch
        {
            JsonTokenType.True => true,
            JsonTokenType.False => false,
            JsonTokenType.Number => reader.GetInt32() != 0,
            JsonTokenType.String => reader.GetString() is "1" or "true",
            _ => false,
        };

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options) =>
        writer.WriteBooleanValue(value);
}
