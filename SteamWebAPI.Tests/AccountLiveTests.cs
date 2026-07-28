using SteamWebAPI.Tests.Support;

namespace SteamWebAPI.Tests;

/// <summary>
/// Calls the real steamwebapi.com API and checks that the response actually deserializes into the library's DTOs.
/// Only <c>GET /account/me</c> is covered here — <c>POST /steam/api/steamloginsecure</c> performs a real Steam
/// login and is out of scope for this test class. Skips automatically if no API key is configured — see
/// <see cref="TestConfig"/>.
/// </summary>
public class AccountLiveTests
{
    [LiveFact]
    public async Task GetAccountAsync_ReturnsAccountInfo()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetAccountAsync();

        // Judgment call: AccountInfo's shape is explicitly documented as inferred/unconfirmed (see its XML doc
        // remarks), so we only assert that the call succeeds and deserializes without throwing, rather than
        // asserting on any specific field.
        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
    }
}
