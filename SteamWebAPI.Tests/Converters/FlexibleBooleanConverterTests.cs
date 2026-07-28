using System.Text.Json;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Tests.Converters;

public class FlexibleBooleanConverterTests
{
    private static readonly JsonSerializerOptions Options = new()
    {
        Converters = { new FlexibleBooleanConverter() },
    };

    [Theory]
    [InlineData("true", true)]
    [InlineData("false", false)]
    [InlineData("1", true)]
    [InlineData("0", false)]
    [InlineData("\"1\"", true)]
    [InlineData("\"0\"", false)]
    [InlineData("\"true\"", true)]
    public void Deserializes_VariousBooleanEncodings(string json, bool expected)
    {
        var result = JsonSerializer.Deserialize<bool>(json, Options);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Serializes_AsJsonBooleanLiteral()
    {
        var json = JsonSerializer.Serialize(true, Options);

        Assert.Equal("true", json);
    }
}
