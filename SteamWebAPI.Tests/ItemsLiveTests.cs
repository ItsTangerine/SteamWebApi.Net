using SteamWebAPI.Models.Common;
using SteamWebAPI.Models.Items;
using SteamWebAPI.Tests.Support;

namespace SteamWebAPI.Tests;

/// <summary>
/// Calls the real steamwebapi.com API and checks that the response actually deserializes into the library's DTOs.
/// Assertions are structural (non-null/non-empty/plausible-range), not exact-value, since live market data changes
/// constantly. Skips automatically if no API key is configured — see <see cref="TestConfig"/>.
/// </summary>
public class ItemsLiveTests
{
    private const string KnownItem = "AK-47 | Redline (Field-Tested)";

    [LiveFact]
    public async Task GetItemsAsync_DefaultOptions_ReturnsParsedItems()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetItemsAsync(new GetItemsRequest { Max = 5 });

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotEmpty(result.Value);
        Assert.All(result.Value, item => Assert.False(string.IsNullOrWhiteSpace(item.MarketHashName)));
    }

    [LiveFact]
    public async Task GetItemsAsync_FilteredAndSorted_ReturnsMatchingItems()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetItemsAsync(new GetItemsRequest
        {
            Game = Game.Cs2,
            ItemGroups = new[] { "container" },
            SortBy = ItemsSortBy.SoldDescending,
            Currency = SteamCurrency.Eur,
            Max = 5,
        });

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotEmpty(result.Value);
        Assert.All(result.Value, item => Assert.Equal("container", item.ItemGroup));
    }

    [LiveFact]
    public async Task GetItemAsync_KnownItem_ReturnsMatchingDto()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetItemAsync(KnownItem);

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.Equal(KnownItem, result.Value.MarketHashName);
        Assert.Equal(Wear.FieldTested, result.Value.Wear);
    }

    [LiveFact]
    public async Task GetItemAsync_WithCurrencyAndGroups_Parses()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetItemAsync(KnownItem, new GetItemRequest
        {
            Currency = SteamCurrency.Eur,
            WithGroups = true,
        });

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
    }

    [LiveFact]
    public async Task GetItemPriceHistoryAsync_KnownItem_Parses()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetItemPriceHistoryAsync(KnownItem, new GetItemPriceHistoryRequest { IntervalDays = 30 });

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
    }

    [LiveFact]
    public async Task GetItemsAggregatedHistoryAsync_KnownItem_Parses()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetItemsAggregatedHistoryAsync(new GetItemsAggregatedHistoryRequest
        {
            Items = new[] { KnownItem },
        });

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
    }

    [LiveFact]
    public async Task GetItemsPreviewAsync_Search_ReturnsMatches()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetItemsPreviewAsync(new GetItemsPreviewRequest { Search = "Redline" });

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotEmpty(result.Value);
    }

    [LiveFact]
    public async Task GetItemsPreviewGroupValuesAsync_ItemType_ReturnsValues()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetItemsPreviewGroupValuesAsync(PreviewGroupBy.ItemType);

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotEmpty(result.Value);
        Assert.Contains("ak-47", result.Value);
    }

    [LiveFact]
    public async Task GetItemOrderActivityAsync_KnownItem_ReturnsJsonElement()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetItemOrderActivityAsync(KnownItem);

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotEqual(default, result.Value.ValueKind);
    }
}
