using System.Text.Json;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Tests.Converters;

public class DateOnlyStringConverterTests
{
    private static readonly JsonSerializerOptions Options = new()
    {
        Converters = { new DateOnlyStringConverter() },
    };

    [Fact]
    public void Deserializes_DateOnlyString()
    {
        var result = JsonSerializer.Deserialize<DateTime?>("\"2025-01-31\"", Options);

        Assert.Equal(new DateTime(2025, 1, 31), result);
    }

    [Fact]
    public void Deserializes_JsonNull_AsNull()
    {
        var result = JsonSerializer.Deserialize<DateTime?>("null", Options);

        Assert.Null(result);
    }

    [Fact]
    public void Serializes_AsDateOnlyString()
    {
        var json = JsonSerializer.Serialize<DateTime?>(new DateTime(2025, 1, 31), Options);

        Assert.Equal("\"2025-01-31\"", json);
    }
}
