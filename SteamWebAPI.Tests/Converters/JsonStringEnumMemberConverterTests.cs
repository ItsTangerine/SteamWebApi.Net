using System.Text.Json;
using SteamWebAPI.Models.Common;

namespace SteamWebAPI.Tests.Converters;

public class JsonStringEnumMemberConverterTests
{
    [Theory]
    [InlineData("\"fn\"", Wear.FactoryNew)]
    [InlineData("\"mw\"", Wear.MinimalWear)]
    [InlineData("\"ft\"", Wear.FieldTested)]
    [InlineData("\"ww\"", Wear.WellWorn)]
    [InlineData("\"bs\"", Wear.BattleScarred)]
    public void Deserializes_KnownWireValues(string json, Wear expected)
    {
        var result = JsonSerializer.Deserialize<Wear>(json);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("\"FN\"")]
    [InlineData("\"Fn\"")]
    public void Deserializes_CaseInsensitively(string json)
    {
        var result = JsonSerializer.Deserialize<Wear>(json);

        Assert.Equal(Wear.FactoryNew, result);
    }

    [Fact]
    public void Deserializes_UnknownValue_ToZeroMemberInsteadOfThrowing()
    {
        var result = JsonSerializer.Deserialize<Wear>("\"not-a-real-wear\"");

        Assert.Equal(default, result);
    }

    [Theory]
    [InlineData(Wear.FactoryNew, "fn")]
    [InlineData(Wear.MinimalWear, "mw")]
    [InlineData(Wear.BattleScarred, "bs")]
    public void Serializes_ToWireValue_NotMemberName(Wear value, string expectedWireValue)
    {
        var json = JsonSerializer.Serialize(value);

        Assert.Equal($"\"{expectedWireValue}\"", json);
    }

    [Theory]
    [InlineData("\"cs2\"", Game.Cs2)]
    [InlineData("\"rust\"", Game.Rust)]
    [InlineData("\"dota\"", Game.Dota)]
    [InlineData("\"tf2\"", Game.Tf2)]
    public void Game_RoundTrips_ThroughWireValues(string json, Game expected)
    {
        var result = JsonSerializer.Deserialize<Game>(json);
        Assert.Equal(expected, result);

        var reserialized = JsonSerializer.Serialize(result);
        Assert.Equal(json, reserialized);
    }

    [Fact]
    public void NullableEnumProperty_DeserializesNull_AsNull()
    {
        var result = JsonSerializer.Deserialize<Wear?>("null");

        Assert.Null(result);
    }
}
