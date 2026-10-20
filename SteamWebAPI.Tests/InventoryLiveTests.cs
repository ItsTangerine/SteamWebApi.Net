using SteamWebAPI.Models.Common;
using SteamWebAPI.Models.Inventory;
using SteamWebAPI.Results;
using SteamWebAPI.Tests.Support;

namespace SteamWebAPI.Tests;

/// <summary>
/// Calls the real steamwebapi.com API and checks that the response actually deserializes into the library's DTOs.
/// Only the enriched (<c>parse=1</c>) single and batch inventory methods are covered here — the undocumented raw
/// (<c>parse=0</c>) mode is out of scope. Skips automatically if no API key/test SteamID is configured — see
/// <see cref="TestConfig"/>.
/// </summary>
public class InventoryLiveTests
{
    // Judgment call: the configured test account's CS2 inventory may legitimately be empty or private.
    // steamwebapi.com reports a private inventory as a domain-specific (SteamApi) error rather than an empty list,
    // so we accept either outcome instead of hard-asserting IsSuccess or a non-empty collection.

    [LiveFact(RequiresSteamId = true)]
    public async Task GetInventoryAsync_DefaultOptions_ReturnsItems()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetInventoryAsync(TestConfig.TestSteamId!, new GetInventoryRequest { Limit = 20 });

        Assert.True(
            result.IsSuccess || result.Error.Type == ErrorType.SteamApi,
            result.IsFailure ? result.Error.ToString() : null);
        if (result.IsSuccess)
            Assert.NotNull(result.Value);
    }

    [LiveFact(RequiresSteamId = true)]
    public async Task GetInventoryAsync_WithPricingAndSorting_ReturnsItems()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetInventoryAsync(TestConfig.TestSteamId!, new GetInventoryRequest
        {
            Currency = SteamCurrency.Usd,
            Sort = InventorySort.PriceMax,
            Group = true,
            WithPrices = true,
            Limit = 10,
        });

        Assert.True(
            result.IsSuccess || result.Error.Type == ErrorType.SteamApi,
            result.IsFailure ? result.Error.ToString() : null);
        if (result.IsSuccess)
        {
            Assert.NotNull(result.Value);
            // The market ident arrives as 'source'; the obsolete 'market' field is no longer sent.
            Assert.All(result.Value.SelectMany(item => item.Prices ?? Array.Empty<InventoryMarketPrice>()), price =>
            {
                Assert.False(string.IsNullOrWhiteSpace(price.Source));
                Assert.True(price.AdditionalData is null || !price.AdditionalData.ContainsKey("market"));
            });
        }
    }

    [LiveFact(RequiresSteamId = true)]
    public async Task GetInventoryPageAsync_FirstPage_ReturnsItemsAndCursorState()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetInventoryPageAsync(TestConfig.TestSteamId!, new GetInventoryRequest { Limit = 20 });

        Assert.True(
            result.IsSuccess || result.Error.Type == ErrorType.SteamApi,
            result.IsFailure ? result.Error.ToString() : null);
        if (result.IsSuccess)
        {
            Assert.NotNull(result.Value.Items);
            Assert.Equal(result.Value.LastAssetId is not null, result.Value.HasMore);
        }
    }

    [LiveFact(RequiresSteamId = true)]
    public async Task GetInventoryAsync_TradeLockedBothFastReturn_IsAccepted()
    {
        using var client = TestConfig.CreateClient();

        // Mode 3 costs +1 credit; a rejected value would come back as a Validation error, not SteamApi.
        var result = await client.GetInventoryAsync(TestConfig.TestSteamId!, new GetInventoryRequest
        {
            State = InventoryState.Active,
            TradeLockedMode = InventoryTradeLockedMode.BothFastReturn,
            Limit = 20,
        });

        Assert.True(
            result.IsSuccess || result.Error.Type == ErrorType.SteamApi,
            result.IsFailure ? result.Error.ToString() : null);
        if (result.IsSuccess)
            Assert.NotNull(result.Value);
    }

    [LiveFact(RequiresSteamId = true)]
    public async Task GetInventoryBatchAsync_ExplicitTradeLockedMode_IsAccepted()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetInventoryBatchAsync(
            new[] { TestConfig.TestSteamId! },
            new GetInventoryBatchRequest { TradeLockedMode = InventoryTradeLockedMode.ContextTwoOnly });

        Assert.True(
            result.IsSuccess || result.Error.Type == ErrorType.SteamApi,
            result.IsFailure ? result.Error.ToString() : null);
        if (result.IsSuccess)
            Assert.NotNull(result.Value);
    }

    [LiveFact(RequiresSteamId = true)]
    public async Task GetInventoryBatchAsync_DefaultOptions_ReturnsItems()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetInventoryBatchAsync(new[] { TestConfig.TestSteamId! });

        Assert.True(
            result.IsSuccess || result.Error.Type == ErrorType.SteamApi,
            result.IsFailure ? result.Error.ToString() : null);
        if (result.IsSuccess)
            Assert.NotNull(result.Value);
    }

    [LiveFact(RequiresSteamId = true)]
    public async Task GetInventoryBatchAsync_WithCurrencyAndGroup_ReturnsItems()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetInventoryBatchAsync(
            new[] { TestConfig.TestSteamId! },
            new GetInventoryBatchRequest { Currency = SteamCurrency.Eur, Group = true, Game = InventoryGame.Cs2 });

        Assert.True(
            result.IsSuccess || result.Error.Type == ErrorType.SteamApi,
            result.IsFailure ? result.Error.ToString() : null);
        if (result.IsSuccess)
            Assert.NotNull(result.Value);
    }
}
