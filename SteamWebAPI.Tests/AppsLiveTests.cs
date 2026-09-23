using SteamWebAPI.Models.Apps;
using SteamWebAPI.Tests.Support;

namespace SteamWebAPI.Tests;

/// <summary>
/// Calls the real steamwebapi.com API and checks that the Apps endpoints respond with JSON. steamwebapi.com publishes
/// no response schema for them, so they return raw JSON and the assertions only check that a payload came back.
/// Skips automatically if no API key is configured — see <see cref="TestConfig"/>.
/// </summary>
public class AppsLiveTests
{
    private const int CounterStrike2AppId = 730;

    [LiveFact]
    public async Task GetAppsAsync_SmallPage_ReturnsJson()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetAppsAsync(new GetAppsRequest { Limit = 5 });

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotEqual(default, result.Value.ValueKind);
    }

    [LiveFact]
    public async Task GetAppMetricsAsync_SmallPage_ReturnsJson()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetAppMetricsAsync(new GetAppsRequest { Limit = 5, SortBy = AppSortBy.Players });

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotEqual(default, result.Value.ValueKind);
    }

    [LiveFact]
    public async Task GetAppFiltersAsync_ReturnsJson()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetAppFiltersAsync();

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotEqual(default, result.Value.ValueKind);
    }

    [LiveFact]
    public async Task GetAppAsync_CounterStrike2_ReturnsJson()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetAppAsync(CounterStrike2AppId);

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotEqual(default, result.Value.ValueKind);
    }

    [LiveFact]
    public async Task GetAppPlayersAsync_CounterStrike2_ReturnsJson()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetAppPlayersAsync(CounterStrike2AppId);

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotEqual(default, result.Value.ValueKind);
    }
}
