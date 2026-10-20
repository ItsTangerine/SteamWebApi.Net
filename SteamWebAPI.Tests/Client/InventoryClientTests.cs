using System.Net;
using SteamWebAPI.Models.Inventory;
using SteamWebAPI.Results;
using SteamWebAPI.Tests.Support;

namespace SteamWebAPI.Tests.Client;

/// <summary>Offline tests for inventory query building, pagination, and DTO mapping.</summary>
public class InventoryClientTests
{
    private const string SteamId = "76561198000000000";

    // Shape taken from the steamwebapi.com contract example for GET /steam/api/inventory, trimmed.
    private const string InventoryJson =
        """
        [{
          "id": "abc123", "markethashname": "AK-47 | Redline (Field-Tested)", "assetid": "41711300816",
          "float": {
            "floatvalue": 0.23456789, "paintseed": 456, "paintindex": 282, "phase": null,
            "stickers": [
              {"slot": 0, "stickerId": 4688, "wear": 0, "name": "iBUYPOWER | Katowice 2014", "image": "https://example.test/a.png"},
              {"slot": 1, "stickerId": 4689, "wear": 0.25, "name": "Titan | Katowice 2014", "image": "https://example.test/b.png"}
            ],
            "keychains": []
          },
          "image": "https://example.test/item.png",
          "prices": [{"source": "skinbaron", "price": 32.5, "quantity": 15, "link": "https://skinbaron.de/x", "variants": null}]
        }]
        """;

    [Theory]
    [InlineData(InventoryTradeLockedMode.ContextTwoOnly, "0")]
    [InlineData(InventoryTradeLockedMode.ContextSixteenOnly, "1")]
    [InlineData(InventoryTradeLockedMode.BothFastReturn, "3")]
    [InlineData(InventoryTradeLockedMode.BothRequired, "4")]
    public async Task GetInventoryAsync_TradeLockedMode_SendsWireValue(InventoryTradeLockedMode mode, string expected)
    {
        var stub = StubHttpMessageHandler.Json("[]");
        using var client = stub.CreateClient();

        await client.GetInventoryAsync(SteamId, new GetInventoryRequest { TradeLockedMode = mode });

        Assert.Equal(expected, stub.LastQuery["trade_locked"]);
    }

    [Theory]
    [InlineData(true, "1")]
    [InlineData(false, "0")]
    public async Task GetInventoryAsync_ObsoleteTradeLocked_StillMapsToContextMode(bool tradeLocked, string expected)
    {
        var stub = StubHttpMessageHandler.Json("[]");
        using var client = stub.CreateClient();

#pragma warning disable CS0618 // Verifying the obsolete shim still works.
        await client.GetInventoryAsync(SteamId, new GetInventoryRequest { TradeLocked = tradeLocked });
#pragma warning restore CS0618

        Assert.Equal(expected, stub.LastQuery["trade_locked"]);
    }

    [Fact]
    public async Task GetInventoryAsync_TradeLockedMode_OverridesObsoleteBool()
    {
        var stub = StubHttpMessageHandler.Json("[]");
        using var client = stub.CreateClient();

#pragma warning disable CS0618 // Verifying precedence over the obsolete shim.
        var request = new GetInventoryRequest { TradeLocked = true, TradeLockedMode = InventoryTradeLockedMode.BothRequired };
#pragma warning restore CS0618
        await client.GetInventoryAsync(SteamId, request);

        Assert.Equal("4", stub.LastQuery["trade_locked"]);
    }

    [Fact]
    public async Task GetInventoryAsync_NoTradeLockedOption_OmitsParam()
    {
        var stub = StubHttpMessageHandler.Json("[]");
        using var client = stub.CreateClient();

        await client.GetInventoryAsync(SteamId);

        Assert.Null(stub.LastQuery["trade_locked"]);
    }

    [Fact]
    public async Task GetInventoryBatchAsync_TradeLockedMode_SendsWireValue()
    {
        var stub = StubHttpMessageHandler.Json("{}");
        using var client = stub.CreateClient();

        await client.GetInventoryBatchAsync(new[] { SteamId }, new GetInventoryBatchRequest { TradeLockedMode = InventoryTradeLockedMode.BothFastReturn });

        Assert.Equal("/steam/api/inventory/batch", stub.LastPath);
        Assert.Equal("3", stub.LastQuery["trade_locked"]);
    }

    [Fact]
    public async Task GetInventoryAsync_StickerWearAndPriceSource_AreMapped()
    {
        var stub = StubHttpMessageHandler.Json(InventoryJson);
        using var client = stub.CreateClient();

        var result = await client.GetInventoryAsync(SteamId);

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        var item = Assert.Single(result.Value);
        Assert.Equal(new double?[] { 0, 0.25 }, item.Float!.Stickers.Select(s => s.Wear));
        Assert.All(item.Float.Stickers, s => Assert.True(s.AdditionalData is null || !s.AdditionalData.ContainsKey("wear")));
        var price = Assert.Single(item.Prices!);
        Assert.Equal("skinbaron", price.Source);
        Assert.Equal("https://skinbaron.de/x", price.Link);
    }

    [Fact]
    public async Task GetInventoryPageAsync_WithLastAssetIdHeader_ReturnsCursor()
    {
        var stub = StubHttpMessageHandler.Json(InventoryJson, headers: new Dictionary<string, string> { ["last_assetid"] = "41711300816" });
        using var client = stub.CreateClient();

        var result = await client.GetInventoryPageAsync(SteamId);

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.Single(result.Value.Items);
        Assert.Equal("41711300816", result.Value.LastAssetId);
        Assert.True(result.Value.HasMore);
    }

    [Fact]
    public async Task GetInventoryPageAsync_WithoutHeader_IsLastPage()
    {
        var stub = StubHttpMessageHandler.Json(InventoryJson);
        using var client = stub.CreateClient();

        var result = await client.GetInventoryPageAsync(SteamId);

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.Null(result.Value.LastAssetId);
        Assert.False(result.Value.HasMore);
    }

    [Fact]
    public async Task GetInventoryPageAsync_SendsSameQueryAsGetInventoryAsync()
    {
        var stub = StubHttpMessageHandler.Json("[]");
        using var client = stub.CreateClient();
        var request = new GetInventoryRequest
        {
            Limit = 100,
            StartAssetId = "41711300816",
            TradeLockedMode = InventoryTradeLockedMode.ContextSixteenOnly,
            WithPrices = true,
        };

        await client.GetInventoryAsync(SteamId, request);
        var expected = stub.LastQuery;
        await client.GetInventoryPageAsync(SteamId, request);
        var actual = stub.LastQuery;

        Assert.Equal("41711300816", actual["start_assetid"]);
        Assert.Equal(
            expected.AllKeys.OrderBy(k => k).Select(k => $"{k}={expected[k]}"),
            actual.AllKeys.OrderBy(k => k).Select(k => $"{k}={actual[k]}"));
    }

    [Fact]
    public async Task GetInventoryPageAsync_ErrorResponse_ReturnsFailure()
    {
        var stub = StubHttpMessageHandler.Json("""{"error":"Inventory is private"}""", HttpStatusCode.BadRequest);
        using var client = stub.CreateClient();

        var result = await client.GetInventoryPageAsync(SteamId);

        Assert.True(result.IsFailure);
        Assert.Equal(ErrorType.Validation, result.Error.Type);
        Assert.Equal("Inventory is private", result.Error.Message);
    }
}
