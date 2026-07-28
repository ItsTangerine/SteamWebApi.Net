using SteamWebAPI.Results;
using SteamWebAPI.Tests.Support;

namespace SteamWebAPI.Tests;

/// <summary>
/// Calls the real steamwebapi.com generic crawl proxy (<see cref="SteamWebApiClient.CrawlAsync"/> and
/// <see cref="SteamWebApiClient.CrawlPremiumAsync"/>). Unlike the other live tests, these don't validate DTO
/// parsing - the proxy returns <c>Task&lt;Result&lt;string&gt;&gt;</c> (a raw string) by design, since the
/// crawled content has no fixed schema, so there's nothing to deserialize.
///
/// The proxy also only crawls URLs steamwebapi.com has pre-whitelisted ("contact Discord to whitelist more" per
/// their docs), which this test project cannot guarantee for any given URL - a Steam-focused API is likely to
/// have steamcommunity.com whitelisted, but that's an assumption, not a guarantee. So these tests can't assert
/// success outright. Instead they assert that the HTTP round-trip itself worked: on success the body is
/// non-null, and on failure the error is anything other than a network/deserialization failure (a whitelist
/// rejection would surface as e.g. a SteamApi/Http/Authentication/Validation error from a completed response,
/// not a Network error from a failed round-trip or a Deserialization error, since there's no typed body to fail
/// to parse here).
/// </summary>
public class ProxyLiveTests
{
    // A Steam Community market listing page - plausible to be whitelisted by default given this is a
    // Steam-focused API, but not guaranteed.
    private const string TargetUrl = "https://steamcommunity.com/market/listings/730/AK-47%20%7C%20Redline%20%28Field-Tested%29";

    [LiveFact]
    public async Task CrawlAsync_WhitelistedUrl_CompletesWithoutTransportFailure()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.CrawlAsync(TargetUrl);

        if (result.IsSuccess)
        {
            Assert.NotNull(result.Value);
        }
        else
        {
            // A whitelist rejection or other API-level failure is an acceptable outcome here - only a
            // network-transport or deserialization failure would indicate something is actually broken.
            Assert.NotEqual(ErrorType.Network, result.Error.Type);
            Assert.NotEqual(ErrorType.Deserialization, result.Error.Type);
        }
    }

    [LiveFact]
    public async Task CrawlPremiumAsync_WhitelistedUrl_CompletesWithoutTransportFailure()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.CrawlPremiumAsync(TargetUrl);

        if (result.IsSuccess)
        {
            Assert.NotNull(result.Value);
        }
        else
        {
            // Premium crawling additionally requires premium account access, so a rejection here is even more
            // likely than for CrawlAsync - still fine, as long as it's not a transport/deserialization failure.
            Assert.NotEqual(ErrorType.Network, result.Error.Type);
            Assert.NotEqual(ErrorType.Deserialization, result.Error.Type);
        }
    }
}
