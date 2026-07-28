using SteamWebAPI.Models.Common;
using SteamWebAPI.Models.MarketPrices;
using SteamWebAPI.Tests.Support;

namespace SteamWebAPI.Tests;

/// <summary>
/// Calls the real steamwebapi.com API and checks that the response actually deserializes into the library's DTOs.
/// These endpoints need no session/account, just the API key plus a market ident and item name, so they always
/// run under <see cref="LiveFact"/> with no extra requirements. Skips automatically if no API key is configured —
/// see <see cref="TestConfig"/>.
/// </summary>
public class MarketPricesLiveTests
{
    private const string KnownItem = "AK-47 | Redline (Field-Tested)";
    private const string KnownMarket = "buff";

    [LiveFact]
    public async Task GetMarketPricesAsync_KnownItem_ReturnsPrice()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetMarketPricesAsync(KnownMarket, new GetMarketPricesRequest { MarketHashName = KnownItem });

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotNull(result.Value);
    }

    [LiveFact]
    public async Task GetMarketPricesAsync_WithCurrency_ReturnsPrice()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetMarketPricesAsync(KnownMarket, new GetMarketPricesRequest
        {
            MarketHashName = KnownItem,
            Currency = SteamCurrency.Eur,
        });

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotNull(result.Value);
    }

    [LiveFact]
    public async Task GetMarketsPricesAsync_KnownItem_ReturnsPricesAcrossMarkets()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetMarketsPricesAsync(new GetMarketsPricesRequest { MarketHashName = KnownItem });

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotEmpty(result.Value);
        Assert.All(result.Value, item => Assert.False(string.IsNullOrWhiteSpace(item.MarketHashName)));
    }

    [LiveFact]
    public async Task GetMarketsPricesAsync_FilteredMarketsAndCurrency_ReturnsPrices()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetMarketsPricesAsync(new GetMarketsPricesRequest
        {
            MarketHashName = KnownItem,
            Markets = new[] { "buff", "skinport" },
            Currency = SteamCurrency.Eur,
        });

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotEmpty(result.Value);
    }

    [LiveFact]
    public async Task GetMarketsHistoryAsync_KnownItem_ReturnsHistory()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetMarketsHistoryAsync(KnownItem);

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.Equal(KnownItem, result.Value.MarketHashName);
    }

    [LiveFact]
    public async Task GetMarketsHistoryAsync_FilteredMarketsAndDateRange_ReturnsHistory()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetMarketsHistoryAsync(KnownItem, new GetMarketsHistoryRequest
        {
            Markets = new[] { "buff", "skinport" },
            StartDate = DateTime.UtcNow.AddDays(-30),
            EndDate = DateTime.UtcNow,
            Currency = SteamCurrency.Eur,
        });

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.Equal(KnownItem, result.Value.MarketHashName);
    }

    [LiveFact]
    public async Task GetMarketHistoryAsync_KnownItem_ReturnsHistory()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetMarketHistoryAsync(KnownMarket, KnownItem);

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotNull(result.Value);
    }

    [LiveFact]
    public async Task GetMarketHistoryAsync_WithDateRangeAndCurrency_ReturnsHistory()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetMarketHistoryAsync(KnownMarket, KnownItem, new GetMarketHistoryRequest
        {
            StartDate = DateTime.UtcNow.AddDays(-30),
            EndDate = DateTime.UtcNow,
            Currency = SteamCurrency.Eur,
        });

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotNull(result.Value);
    }
}
