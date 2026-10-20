using SteamWebAPI.Models.Profile;
using SteamWebAPI.Results;
using SteamWebAPI.Tests.Support;

namespace SteamWebAPI.Tests;

/// <summary>
/// Calls the real steamwebapi.com API and checks that the response actually deserializes into the library's DTOs.
/// Assertions are structural, not exact-value. Skips automatically if no API key/test SteamID/test trade URL is
/// configured — see <see cref="TestConfig"/>.
/// </summary>
public class ProfileLiveTests
{
    [LiveFact(RequiresSteamId = true)]
    public async Task GetFriendListAsync_ReturnsFriends()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetFriendListAsync(TestConfig.TestSteamId!);

        // Judgment call: the configured test account's friends list may legitimately be private, which
        // steamwebapi.com reports as a domain-specific (SteamApi) error rather than an empty list. We treat that
        // as an acceptable documented outcome rather than a test failure, since we don't control the test account.
        Assert.True(
            result.IsSuccess || result.Error.Type == ErrorType.SteamApi,
            result.IsFailure ? result.Error.ToString() : null);
        if (result.IsSuccess)
            Assert.NotNull(result.Value);
    }

    [LiveFact(RequiresSteamId = true)]
    public async Task GetProfileAsync_DefaultOptions_ReturnsProfile()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetProfileAsync(TestConfig.TestSteamId!);

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.False(string.IsNullOrWhiteSpace(result.Value.SteamId));
        Assert.False(string.IsNullOrWhiteSpace(result.Value.PersonaName));
    }

    [LiveFact(RequiresSteamId = true)]
    public async Task GetProfileAsync_FullWithGroups_ReturnsProfile()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetProfileAsync(TestConfig.TestSteamId!, new GetProfileRequest
        {
            State = ProfileState.Full,
            WithGroups = true,
        });

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.False(string.IsNullOrWhiteSpace(result.Value.SteamId));
    }

    [LiveFact(RequiresSteamId = true)]
    public async Task GetProfileBatchAsync_DefaultOptions_ReturnsProfiles()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetProfileBatchAsync(new[] { TestConfig.TestSteamId! });

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotNull(result.Value.Response.Players);
        Assert.NotEmpty(result.Value.Response.Players);
    }

    [LiveFact(RequiresSteamId = true)]
    public async Task GetProfileBatchAsync_FullWithGroups_ReturnsProfiles()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetProfileBatchAsync(
            new[] { TestConfig.TestSteamId! },
            new GetProfileBatchRequest { State = ProfileState.Full, WithGroups = true });

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotEmpty(result.Value.Response.Players);
    }

    [LiveFact(RequiresSteamId = true)]
    public async Task GetProfileGamesAsync_ReturnsGames()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetProfileGamesAsync(TestConfig.TestSteamId!);

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.False(string.IsNullOrWhiteSpace(result.Value.SteamId));
        Assert.NotNull(result.Value.Games);
    }

    [LiveFact(RequiresSteamId = true)]
    public async Task GetProfileRiskAsync_ReturnsAssessment()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetProfileRiskAsync(TestConfig.TestSteamId!);

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.InRange(result.Value.Risk.Score, 0, 100);
    }

    [LiveFact(RequiresSteamId = true)]
    public async Task GetProfileTradesAsync_SmallPage_ReturnsHistory()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetProfileTradesAsync(TestConfig.TestSteamId!, new GetProfileTradesRequest { Limit = 5 });

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotNull(result.Value.Data);
        Assert.True(result.Value.Data.Count <= 5);
    }

    [LiveFact(RequiresTradeUrl = true)]
    public async Task GetTradeEligibilityAsync_ReturnsEligibility()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetTradeEligibilityAsync(TestConfig.TestTradeUrl!);

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.False(string.IsNullOrWhiteSpace(result.Value.Status));
        // 'message' is undocumented but returned live; it must land on Message, not in AdditionalData.
        Assert.True(result.Value.AdditionalData is null || !result.Value.AdditionalData.ContainsKey("message"));
    }
}
