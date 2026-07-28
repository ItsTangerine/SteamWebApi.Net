using SteamWebAPI.Tests.Support;

namespace SteamWebAPI.Tests;

/// <summary>
/// Calls the real steamwebapi.com API and checks that the response actually deserializes. Both endpoints return raw
/// <see cref="System.Text.Json.JsonElement"/> payloads since steamwebapi.com doesn't publish a response schema for
/// them, so assertions are limited to "the call succeeded and returned real JSON". Skips automatically if no API
/// key is configured — see <see cref="TestConfig"/>.
/// </summary>
public class CurrencyLiveTests
{
    [LiveFact]
    public async Task GetCurrenciesAsync_DefaultBase_ReturnsCurrencyList()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetCurrenciesAsync();

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotEqual(default, result.Value.ValueKind);
    }

    [LiveFact]
    public async Task GetExchangeRateAsync_UsdToEur_ReturnsRate()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetExchangeRateAsync("EUR");

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotEqual(default, result.Value.ValueKind);
    }
}
