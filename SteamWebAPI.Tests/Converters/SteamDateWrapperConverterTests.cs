using System.Text.Json;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Tests.Converters;

public class SteamDateWrapperConverterTests
{
    private static readonly JsonSerializerOptions Options = new()
    {
        Converters = { new SteamDateWrapperConverter() },
    };

    [Fact]
    public void Deserializes_PhpStyleWrapperObject()
    {
        const string json = """{"date":"2025-10-24 12:37:31.000000","timezone_type":3,"timezone":"UTC"}""";

        var result = JsonSerializer.Deserialize<DateTimeOffset?>(json, Options);

        Assert.NotNull(result);
        Assert.Equal(new DateTimeOffset(2025, 10, 24, 12, 37, 31, TimeSpan.Zero), result);
    }

    [Fact]
    public void Deserializes_PlainIsoString()
    {
        const string json = "\"2025-10-24T12:37:31+00:00\"";

        var result = JsonSerializer.Deserialize<DateTimeOffset?>(json, Options);

        Assert.NotNull(result);
        Assert.Equal(new DateTimeOffset(2025, 10, 24, 12, 37, 31, TimeSpan.Zero), result);
    }

    [Fact]
    public void Deserializes_JsonNull_AsNull()
    {
        var result = JsonSerializer.Deserialize<DateTimeOffset?>("null", Options);

        Assert.Null(result);
    }

    [Fact]
    public void Deserializes_EmptyDateInWrapper_AsNull()
    {
        const string json = """{"date":"","timezone_type":3,"timezone":"UTC"}""";

        var result = JsonSerializer.Deserialize<DateTimeOffset?>(json, Options);

        Assert.Null(result);
    }

    [Fact]
    public void Serializes_AsIso8601String()
    {
        var value = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero);

        var json = JsonSerializer.Serialize<DateTimeOffset?>(value, Options);

        Assert.Contains("2025-01-01", json);
    }
}
