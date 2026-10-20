using System.Net;
using SteamWebAPI.Results;
using SteamWebAPI.Tests.Support;

namespace SteamWebAPI.Tests.Client;

/// <summary>Offline tests for the profile endpoints' DTO mapping and error handling.</summary>
public class ProfileClientTests
{
    [Fact]
    public async Task GetTradeEligibilityAsync_NullMessage_MapsEveryField()
    {
        var stub = StubHttpMessageHandler.Json(
            """{"tradeurlvalid":true,"escrowdays":0,"isescrow":false,"status":"instant","message":null}""");
        using var client = stub.CreateClient();

        var result = await client.GetTradeEligibilityAsync("https://steamcommunity.com/tradeoffer/new/?partner=1&token=abc");

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.True(result.Value.TradeUrlValid);
        Assert.Equal(0, result.Value.EscrowDays);
        Assert.False(result.Value.IsEscrow);
        Assert.Equal("instant", result.Value.Status);
        Assert.Null(result.Value.Message);
        Assert.True(result.Value.AdditionalData is null || result.Value.AdditionalData.Count == 0);
        Assert.Equal("/steam/api/profile/trade-eligibility", stub.LastPath);
    }

    [Fact]
    public async Task GetTradeEligibilityAsync_WithMessage_MapsMessage()
    {
        var stub = StubHttpMessageHandler.Json(
            """{"tradeurlvalid":false,"escrowdays":15,"isescrow":true,"status":"escrow","message":"Trade will be held"}""");
        using var client = stub.CreateClient();

        var result = await client.GetTradeEligibilityAsync("https://steamcommunity.com/tradeoffer/new/?partner=1&token=abc");

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.Equal("Trade will be held", result.Value.Message);
        Assert.Equal(15, result.Value.EscrowDays);
    }

    [Fact]
    public async Task GetTradeEligibilityAsync_LegacyErrorBody_KeepsErrorMessage()
    {
        var stub = StubHttpMessageHandler.Json("""{"error":"Invalid trade URL"}""", HttpStatusCode.BadRequest);
        using var client = stub.CreateClient();

        var result = await client.GetTradeEligibilityAsync("not-a-trade-url");

        Assert.True(result.IsFailure);
        Assert.Equal(ErrorType.Validation, result.Error.Type);
        Assert.Equal("Invalid trade URL", result.Error.Message);
    }
}
