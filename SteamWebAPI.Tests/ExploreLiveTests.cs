using SteamWebAPI.Models.Explore;
using SteamWebAPI.Tests.Support;

namespace SteamWebAPI.Tests;

/// <summary>
/// Calls the real steamwebapi.com API and checks that the response actually deserializes into the library's DTOs.
/// Assertions are structural, not exact-value: public profile search results change over time and may legitimately
/// be empty. Skips automatically if no API key is configured — see <see cref="TestConfig"/>.
/// </summary>
public class ExploreLiveTests
{
    [LiveFact]
    public async Task GetExploreProfilesAsync_DefaultOptions_ReturnsParsedProfiles()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetExploreProfilesAsync(new GetExploreProfilesRequest { Limit = 5 });

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotNull(result.Value);
        Assert.All(result.Value, profile => Assert.False(string.IsNullOrWhiteSpace(profile.SteamId)));
    }

    [LiveFact]
    public async Task GetExploreProfilesAsync_SortedByWorthDescending_ReturnsParsedProfiles()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetExploreProfilesAsync(new GetExploreProfilesRequest
        {
            Limit = 5,
            OrderByField = ExploreProfileOrderByField.Worth,
            OrderByDescending = true,
        });

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotNull(result.Value);
        Assert.All(result.Value, profile => Assert.False(string.IsNullOrWhiteSpace(profile.SteamId)));
    }
}
