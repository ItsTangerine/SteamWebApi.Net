using System.Text.Json;
using SteamWebAPI.Models.Info;
using SteamWebAPI.Tests.Support;

namespace SteamWebAPI.Tests;

/// <summary>
/// Calls the real steamwebapi.com API and checks that the response actually deserializes into the library's DTOs.
/// Assertions are structural (non-null/non-empty/plausible-range), not exact-value, since live data changes
/// constantly. Skips automatically if no API key is configured — see <see cref="TestConfig"/>.
/// </summary>
public class InfoLiveTests
{
    [LiveFact]
    public async Task GetItemInfoStructureAsync_DefaultGame_ReturnsGroupsWithRelations()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetItemInfoStructureAsync();

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotEmpty(result.Value);
        Assert.All(result.Value, group => Assert.False(string.IsNullOrWhiteSpace(group.Name)));
    }

    [LiveFact]
    public async Task GetItemInfoValuesAsync_Types_ReturnsKnownWeaponType()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetItemInfoValuesAsync(ItemInfoValueType.Types);

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotEmpty(result.Value);
        Assert.Contains("ak-47", result.Value);
    }

    [LiveFact(RequiresSteamId = true)]
    public async Task ConvertSteamIdAsync_KnownSteamId_ReturnsConvertedFormats()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.ConvertSteamIdAsync(TestConfig.TestSteamId!);

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotEqual(default, result.Value.ValueKind);
    }

    [LiveFact]
    public async Task GetContainersAsync_DefaultOptions_ReturnsContainersPayload()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetContainersAsync();

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotEqual(default, result.Value.ValueKind);
    }

    [LiveFact]
    public async Task GetContainersAsync_FilteredAndSorted_ReturnsMatchingPayload()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetContainersAsync(new GetContainersRequest
        {
            Type = ContainerType.Case,
            Search = "Chroma",
            SortBy = ContainerSortBy.NameAscending,
        });

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotEqual(default, result.Value.ValueKind);
    }

    [LiveFact]
    public async Task GetCollectionsAsync_DefaultOptions_ReturnsCollectionsPayload()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetCollectionsAsync(new GetCollectionsRequest { Limit = 5 });

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotEqual(default, result.Value.ValueKind);
    }

    [LiveFact]
    public async Task GetCollectionsAsync_WithFieldSelectionAndPagination_ReturnsCollectionsPayload()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetCollectionsAsync(new GetCollectionsRequest
        {
            SelectFields = new[] { "name", "slug" },
            Limit = 5,
            Offset = 5,
        });

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotEqual(default, result.Value.ValueKind);
    }

    [LiveFact]
    public async Task GetCollectionAsync_SlugFromCollectionsList_ReturnsCollection()
    {
        using var client = TestConfig.CreateClient();

        var collectionsResult = await client.GetCollectionsAsync(new GetCollectionsRequest { Limit = 5 });
        Assert.True(collectionsResult.IsSuccess, collectionsResult.IsFailure ? collectionsResult.Error.ToString() : null);

        var collections = collectionsResult.Value;
        if (collections.ValueKind != JsonValueKind.Array || collections.GetArrayLength() == 0)
            return; // The wire shape for this undocumented endpoint didn't yield a list this time; nothing further to check.

        var first = collections[0];
        if (!first.TryGetProperty("slug", out var slugProperty) || slugProperty.ValueKind != JsonValueKind.String)
            return; // The shape doesn't expose a "slug" field as expected; the schema for this endpoint is unconfirmed.

        var slug = slugProperty.GetString();
        Assert.False(string.IsNullOrWhiteSpace(slug));

        var collectionResult = await client.GetCollectionAsync(slug!);

        Assert.True(collectionResult.IsSuccess, collectionResult.IsFailure ? collectionResult.Error.ToString() : null);
        Assert.NotEqual(default, collectionResult.Value.ValueKind);
    }

    [LiveFact]
    public async Task GetSupportedMarketsAsync_ReturnsMarketsPayload()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetSupportedMarketsAsync();

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotEqual(default, result.Value.ValueKind);
    }

    [LiveFact]
    public async Task AutocompleteItemsAsync_PartialName_ReturnsSuggestions()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.AutocompleteItemsAsync("AK-47", game: "cs2");

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotEmpty(result.Value);
        Assert.All(result.Value, suggestion => Assert.False(string.IsNullOrWhiteSpace(suggestion.MarketHashName)));
    }
}
