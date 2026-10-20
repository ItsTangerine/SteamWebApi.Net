using System.Text.Json;
using SteamWebAPI.Models.Float;
using SteamWebAPI.Tests.Support;

namespace SteamWebAPI.Tests.Client;

/// <summary>Offline tests for the float endpoints' request bodies.</summary>
public class FloatClientTests
{
    [Fact]
    public async Task CreateInspectLinkAsync_Keychain_SendsPlacementFieldsAndOmitsNulls()
    {
        var stub = StubHttpMessageHandler.Json("""{"inspectlink":"steam://x","certificate":"00","decoded":{}}""");
        using var client = stub.CreateClient();

        await client.CreateInspectLinkAsync(new CreateInspectLinkRequest
        {
            DefIndex = 7,
            Keychains = new[]
            {
                new CreateInspectKeychain
                {
                    StickerId = 1,
                    Pattern = 42,
                    Wear = 0.1,
                    Scale = 1.5,
                    Rotation = 90,
                    TintId = 3,
                    OffsetX = 0.25,
                    OffsetY = -0.5,
                    OffsetZ = 2,
                },
                new CreateInspectKeychain { StickerId = 2 },
            },
        });

        using var body = JsonDocument.Parse(stub.RequestBodies[^1]!);
        var keychains = body.RootElement.GetProperty("keychains");

        var full = keychains[0];
        Assert.Equal(1, full.GetProperty("sticker_id").GetInt32());
        Assert.Equal(42, full.GetProperty("pattern").GetInt32());
        Assert.Equal(0.1, full.GetProperty("wear").GetDouble());
        Assert.Equal(1.5, full.GetProperty("scale").GetDouble());
        Assert.Equal(90, full.GetProperty("rotation").GetDouble());
        Assert.Equal(3, full.GetProperty("tint_id").GetInt32());
        Assert.Equal(0.25, full.GetProperty("offset_x").GetDouble());
        Assert.Equal(-0.5, full.GetProperty("offset_y").GetDouble());
        Assert.Equal(2, full.GetProperty("offset_z").GetDouble());

        var minimal = keychains[1];
        Assert.Equal(new[] { "sticker_id" }, minimal.EnumerateObject().Select(p => p.Name));
    }
}
