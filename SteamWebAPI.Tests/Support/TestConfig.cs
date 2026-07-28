using Microsoft.Extensions.Configuration;

namespace SteamWebAPI.Tests.Support;

/// <summary>
/// Loads the secrets live tests need (steamwebapi.com API key, a test SteamID, an optional trade URL) from user
/// secrets first, falling back to environment variables so the same tests work in CI.
/// </summary>
/// <remarks>
/// Configure locally with, e.g.:
/// <code>
/// dotnet user-secrets set "SteamWebApi:ApiKey" "your-steamwebapi-com-key" --project SteamWebAPI.Tests
/// dotnet user-secrets set "SteamWebApi:TestSteamId" "76561198000000000" --project SteamWebAPI.Tests
/// dotnet user-secrets set "SteamWebApi:TestTradeUrl" "https://steamcommunity.com/tradeoffer/new/?partner=...&amp;token=..." --project SteamWebAPI.Tests
/// </code>
/// or the equivalent environment variables <c>STEAMWEBAPI_API_KEY</c>, <c>STEAMWEBAPI_TEST_STEAMID</c>,
/// <c>STEAMWEBAPI_TEST_TRADE_URL</c>.
/// </remarks>
internal static class TestConfig
{
    private static readonly IConfigurationRoot Configuration = new ConfigurationBuilder()
        .AddUserSecrets(typeof(TestConfig).Assembly, optional: true)
        .AddEnvironmentVariables()
        .Build();

    /// <summary>The steamwebapi.com API key used by every live test.</summary>
    public static string? ApiKey =>
        NullIfEmpty(Configuration["SteamWebApi:ApiKey"]) ?? NullIfEmpty(Environment.GetEnvironmentVariable("STEAMWEBAPI_API_KEY"));

    /// <summary>A SteamID64 with a public profile/inventory, used as the subject for profile/inventory/friendlist tests.</summary>
    public static string? TestSteamId =>
        NullIfEmpty(Configuration["SteamWebApi:TestSteamId"]) ?? NullIfEmpty(Environment.GetEnvironmentVariable("STEAMWEBAPI_TEST_STEAMID"));

    /// <summary>An optional Steam trade URL, used only by the trade-eligibility test.</summary>
    public static string? TestTradeUrl =>
        NullIfEmpty(Configuration["SteamWebApi:TestTradeUrl"]) ?? NullIfEmpty(Environment.GetEnvironmentVariable("STEAMWEBAPI_TEST_TRADE_URL"));

    /// <summary>
    /// The request budget the whole live-test run throttles itself to, shared across every test via
    /// <see cref="RateLimitingHandler"/>. Defaults to 5/minute, matching steamwebapi.com's lowest-tier plan.
    /// Raise it with the 'SteamWebApi:MaxRequestsPerMinute' user secret (or STEAMWEBAPI_MAX_RPM env var) if your
    /// plan allows more.
    /// </summary>
    public static int MaxRequestsPerMinute
    {
        get
        {
            var raw = NullIfEmpty(Configuration["SteamWebApi:MaxRequestsPerMinute"]) ?? NullIfEmpty(Environment.GetEnvironmentVariable("STEAMWEBAPI_MAX_RPM"));
            return raw is not null && int.TryParse(raw, out var value) && value > 0 ? value : 5;
        }
    }

    public static bool HasApiKey => ApiKey is not null;
    public static bool HasTestSteamId => TestSteamId is not null;
    public static bool HasTestTradeUrl => TestTradeUrl is not null;

    /// <summary>
    /// Creates a client wired to the real steamwebapi.com host, throttled to <see cref="MaxRequestsPerMinute"/>,
    /// for use by <see cref="LiveFactAttribute"/>-marked tests.
    /// </summary>
    public static SteamWebApiClient CreateClient()
    {
        var apiKey = ApiKey ?? throw new InvalidOperationException(
            "No API key configured. This should only be called from a test guarded by [LiveFact].");

        var httpClient = new HttpClient(new RateLimitingHandler(new HttpClientHandler()))
        {
            BaseAddress = new Uri("https://www.steamwebapi.com"),
        };

        return new SteamWebApiClient(apiKey, httpClient);
    }

    private static string? NullIfEmpty(string? value) => string.IsNullOrWhiteSpace(value) ? null : value;
}
