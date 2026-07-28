using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Common;

namespace SteamWebAPI.Models.Converters;

/// <summary>
/// Reads a <c>latest10steamsales</c> entry, which is encoded as a heterogeneous 3-element JSON array
/// <c>[date, price, quantity]</c> rather than an object.
/// </summary>
public sealed class SteamSaleConverter : JsonConverter<SteamSale>
{
    private const string DateFormat = "yyyy-MM-dd";

    /// <inheritdoc />
    public override SteamSale Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
            throw new JsonException("Expected a 3-element array for a steamwebapi.com sale entry.");

        reader.Read();
        var dateStr = reader.GetString();
        reader.Read();
        var price = reader.GetDecimal();
        reader.Read();
        var quantity = reader.GetInt32();
        reader.Read(); // EndArray

        var date = DateTime.TryParseExact(dateStr, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out var parsed)
            ? new DateTimeOffset(parsed, TimeSpan.Zero)
            : default;

        return new SteamSale(date, price, quantity);
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, SteamSale value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        writer.WriteStringValue(value.Date.ToString(DateFormat, CultureInfo.InvariantCulture));
        writer.WriteNumberValue(value.Price);
        writer.WriteNumberValue(value.Quantity);
        writer.WriteEndArray();
    }
}
