using System.Text.Json;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Tests.Converters;

public class UnixSecondsConverterTests
{
    private static readonly JsonSerializerOptions Options = new()
    {
        Converters = { new UnixSecondsConverter() },
    };

    [Fact]
    public void Deserializes_NumericUnixSeconds()
    {
        var result = JsonSerializer.Deserialize<DateTimeOffset?>("1421625600", Options);

        Assert.Equal(DateTimeOffset.FromUnixTimeSeconds(1421625600), result);
    }

    [Fact]
    public void Deserializes_StringEncodedUnixSeconds()
    {
        var result = JsonSerializer.Deserialize<DateTimeOffset?>("\"1421625600\"", Options);

        Assert.Equal(DateTimeOffset.FromUnixTimeSeconds(1421625600), result);
    }

    [Fact]
    public void Deserializes_JsonNull_AsNull()
    {
        var result = JsonSerializer.Deserialize<DateTimeOffset?>("null", Options);

        Assert.Null(result);
    }

    [Fact]
    public void Serializes_AsUnixSecondsNumber()
    {
        var value = DateTimeOffset.FromUnixTimeSeconds(1421625600);

        var json = JsonSerializer.Serialize<DateTimeOffset?>(value, Options);

        Assert.Equal("1421625600", json);
    }
}
