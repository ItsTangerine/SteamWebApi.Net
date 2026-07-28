using SteamWebAPI.Models.MarketIndex;
using SteamWebAPI.Tests.Support;

namespace SteamWebAPI.Tests;

/// <summary>
/// Calls the real steamwebapi.com API and checks that the response actually deserializes into the library's DTOs.
/// These endpoints need no session/account, just the API key, so they always run under <see cref="LiveFact"/> with
/// no extra requirements. Skips automatically if no API key is configured — see <see cref="TestConfig"/>.
/// </summary>
public class MarketIndexLiveTests
{
    [LiveFact]
    public async Task GetMarketIndexAsync_GlobalOverview_ReturnsIndex()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetMarketIndexAsync();

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.Equal("global", result.Value.Mode);
        Assert.True(result.Value.GameId > 0);
    }

    [LiveFact]
    public async Task GetMarketIndexAsync_Segment_ReturnsSegmentStats()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetMarketIndexAsync(new GetMarketIndexRequest
        {
            SegmentType = MarketIndexSegmentType.ItemGroup,
            SegmentKey = "knife",
        });

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.Equal("segment", result.Value.Mode);
    }

    [LiveFact]
    public async Task GetMarketIndexHistoryAsync_DefaultOptions_ReturnsHistory()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetMarketIndexHistoryAsync(new GetMarketIndexHistoryRequest { Limit = 10 });

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotNull(result.Value.History);
    }

    [LiveFact]
    public async Task GetMarketIndexHistoryAsync_FilteredBySegmentAndInterval_ReturnsHistory()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetMarketIndexHistoryAsync(new GetMarketIndexHistoryRequest
        {
            Metric = MarketIndexMetric.Sold24h,
            Interval = MarketIndexInterval.Daily,
            SegmentType = MarketIndexHistorySegmentType.ItemGroup,
            SegmentKey = "knife",
            Limit = 10,
        });

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotNull(result.Value.History);
    }

    [LiveFact]
    public async Task GetMarketIndexMultiMetricHistoryAsync_DefaultOptions_ReturnsHistory()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetMarketIndexMultiMetricHistoryAsync(
            new[] { MarketIndexMetric.PriceIndex, MarketIndexMetric.Sold24h },
            new GetMarketIndexMultiMetricHistoryRequest { Limit = 10 });

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotNull(result.Value.History);
    }

    [LiveFact]
    public async Task GetMarketIndexMultiMetricHistoryAsync_FilteredBySegment_ReturnsHistory()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetMarketIndexMultiMetricHistoryAsync(
            new[] { MarketIndexMetric.PriceIndex, MarketIndexMetric.Listings },
            new GetMarketIndexMultiMetricHistoryRequest
            {
                Interval = MarketIndexInterval.Daily,
                SegmentType = MarketIndexHistorySegmentType.ItemGroup,
                SegmentKey = "knife",
                Limit = 10,
            });

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotNull(result.Value.History);
    }

    [LiveFact]
    public async Task GetMarketIndexCompareAsync_DefaultOptions_ReturnsComparison()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetMarketIndexCompareAsync(MarketIndexSegmentType.ItemGroup);

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotNull(result.Value.Segments);
    }

    [LiveFact]
    public async Task GetMarketIndexCompareAsync_FilteredByKeysAndMetric_ReturnsComparison()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetMarketIndexCompareAsync(MarketIndexSegmentType.ItemGroup, new GetMarketIndexCompareRequest
        {
            Keys = new[] { "knife", "rifle" },
            Metric = MarketIndexCompareMetric.Sold24h,
        });

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotNull(result.Value.Segments);
    }
}
