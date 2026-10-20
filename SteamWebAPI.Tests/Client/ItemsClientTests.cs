using SteamWebAPI.Tests.Support;

namespace SteamWebAPI.Tests.Client;

/// <summary>Offline tests for <see cref="Models.Common.SteamItem"/> mapping against the live response shape.</summary>
public class ItemsClientTests
{
    // Field names and types captured from a live GET /steam/api/items response (2026-10-20), trimmed. The live API
    // returns 'itemimage'/'marketvolume' even though the published contract example shows 'image'.
    private const string LiveShapeItemsJson =
        """
        [{
          "id": "4bb7c73c",
          "markethashname": "Sticker | GuardiaN (Gold) | London 2018",
          "releasedat": null,
          "marketvolume": 1234.5,
          "itemimage": "https://community.akamai.steamstatic.com/economy/image/abc",
          "inspectlink": "steam://run/730//+csgo_econ_action_preview%20C4D4C4DC7DCDE4C",
          "prices": [{
            "price": 188.63, "source": "csfloat", "quantity": 5, "name": "CSFloat",
            "logo": "https://example.test/csfloat.png", "type": "offer", "created_at": "2026-10-20 13:25:01",
            "tags": [], "winloss": null, "link": "https://www.steamwebapi.com/ref/market/14/x", "variants": null
          }]
        }]
        """;

    // Field names and types captured from a live GET /steam/api/item response (2026-10-20), trimmed. Unlike
    // /steam/api/items, this endpoint sends 'image' (not 'itemimage') and richer per-market entries.
    private const string LiveShapeSingleItemJson =
        """
        {
          "markethashname": "AK-47 | Redline (Field-Tested)",
          "releasedat": {"date": "2014-02-21 00:00:00.000000", "timezone_type": 3, "timezone": "UTC"},
          "infoprice": "priceXXXX = Steam Prices read docs https://www.steamwebapi.com/api/doc - deprecated fields",
          "infopricereal": "pricerealXXXX = Third Market Prices read docs https://www.steamwebapi.com/api/doc",
          "marketvolume": 99.5,
          "image": "https://community.cloudflare.steamstatic.com/economy/image/def",
          "prices": [{
            "source": "skinport", "name": "Skinport", "trustscore": 4.9, "totalitems": null, "usagecount": 1130198,
            "isp2p": false, "istrusted": true, "trustscorecount": 33900, "type": "offer", "tags": [],
            "createdat": "2026-10-20 13:35:01", "price": 12.5, "logo": "https://example.test/skinport.png", "quantity": 40,
            "saleFees": [], "buyFees": [], "payoutFees": [], "created_at": "2026-10-20 13:35:01", "winloss": null,
            "link": "https://www.steamwebapi.com/ref/market/3/x", "variants": null
          }]
        }
        """;

    [Fact]
    public async Task GetItemAsync_LiveShape_MapsImageSpellingAndInfoFields()
    {
        var stub = StubHttpMessageHandler.Json(LiveShapeSingleItemJson);
        using var client = stub.CreateClient();

        var result = await client.GetItemAsync("AK-47 | Redline (Field-Tested)");

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        var item = result.Value;
        Assert.Equal("https://community.cloudflare.steamstatic.com/economy/image/def", item.Image);
        Assert.StartsWith("priceXXXX", item.InfoPrice);
        Assert.StartsWith("pricerealXXXX", item.InfoPriceReal);
        Assert.Equal(new DateTimeOffset(2014, 2, 21, 0, 0, 0, TimeSpan.Zero), item.ReleasedAt);
        Assert.Equal(99.5, item.MarketVolume);
        Assert.True(item.AdditionalData is null || item.AdditionalData.Keys.Intersect(new[] { "image", "infoprice", "infopricereal" }).Count() == 0);
    }

    [Fact]
    public async Task GetItemAsync_LiveShape_MapsPriceEntryTrustFields()
    {
        var stub = StubHttpMessageHandler.Json(LiveShapeSingleItemJson);
        using var client = stub.CreateClient();

        var result = await client.GetItemAsync("AK-47 | Redline (Field-Tested)");

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        var price = Assert.Single(result.Value.Prices!);
        Assert.Equal("skinport", price.Source);
        Assert.Equal(4.9, price.TrustScore);
        Assert.Equal(33900, price.TrustScoreCount);
        Assert.Equal(1130198L, price.UsageCount);
        Assert.False(price.IsP2P);
        Assert.True(price.IsTrusted);
        Assert.Equal(new DateTimeOffset(2026, 10, 20, 13, 35, 1, TimeSpan.Zero), price.CreatedAt);
        // Fields with no observed non-empty values yet are preserved, not typed.
        Assert.NotNull(price.AdditionalData);
        Assert.Equal(
            new[] { "buyFees", "createdat", "payoutFees", "saleFees", "tags", "totalitems", "winloss" },
            price.AdditionalData!.Keys.OrderBy(k => k, StringComparer.Ordinal));
    }

    [Fact]
    public async Task GetItemsAsync_LiveShape_MapsImageVolumeAndInspectLink()
    {
        var stub = StubHttpMessageHandler.Json(LiveShapeItemsJson);
        using var client = stub.CreateClient();

        var result = await client.GetItemsAsync();

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        var item = Assert.Single(result.Value);
        Assert.Equal("https://community.akamai.steamstatic.com/economy/image/abc", item.Image);
        Assert.Equal(1234.5, item.MarketVolume);
        Assert.Equal("steam://run/730//+csgo_econ_action_preview%20C4D4C4DC7DCDE4C", item.InspectLink);
        Assert.Null(item.ReleasedAt);
        Assert.True(item.AdditionalData is null || item.AdditionalData.Keys.Intersect(new[] { "itemimage", "marketvolume", "inspectlink", "releasedat" }).Count() == 0);
    }

    [Fact]
    public async Task GetItemsAsync_ReleasedAtWrapper_ParsesUtcDate()
    {
        var stub = StubHttpMessageHandler.Json(
            """[{"markethashname":"AK-47 | Redline (Field-Tested)","releasedat":{"date":"2026-07-08 00:00:00.000000","timezone_type":3,"timezone":"UTC"}}]""");
        using var client = stub.CreateClient();

        var result = await client.GetItemsAsync();

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.Equal(new DateTimeOffset(2026, 7, 8, 0, 0, 0, TimeSpan.Zero), Assert.Single(result.Value).ReleasedAt);
    }

    [Fact]
    public async Task GetItemsAsync_PriceEntry_MapsLiveFieldsAndKeepsUntypedOnes()
    {
        var stub = StubHttpMessageHandler.Json(LiveShapeItemsJson);
        using var client = stub.CreateClient();

        var result = await client.GetItemsAsync();

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        var price = Assert.Single(Assert.Single(result.Value).Prices!);
        Assert.Equal("csfloat", price.Source);
        Assert.Equal("CSFloat", price.Name);
        Assert.Equal("https://example.test/csfloat.png", price.Logo);
        Assert.Equal("offer", price.Type);
        Assert.Equal("https://www.steamwebapi.com/ref/market/14/x", price.Link);
        Assert.Equal(new DateTimeOffset(2026, 10, 20, 13, 25, 1, TimeSpan.Zero), price.CreatedAt);
        Assert.Equal(188.63, price.Price);
        Assert.Equal(5, price.Quantity);
        // 'tags' and 'winloss' have no observed non-empty values yet, so they are preserved rather than typed.
        Assert.NotNull(price.AdditionalData);
        Assert.True(price.AdditionalData!.ContainsKey("tags"));
        Assert.True(price.AdditionalData.ContainsKey("winloss"));
#pragma warning disable CS0618 // Market is obsolete; the API only sends 'source'.
        Assert.Equal(string.Empty, price.Market);
#pragma warning restore CS0618
    }
}
