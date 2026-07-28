namespace SteamWebAPI.Tests.Support;

/// <summary>
/// Marks a test that makes a real HTTP call to steamwebapi.com. Skips automatically (with an explanatory reason,
/// visible in test output) when the required secrets aren't configured, rather than failing.
/// </summary>
public sealed class LiveFactAttribute : FactAttribute
{
    /// <summary>Whether this test additionally requires <see cref="TestConfig.TestSteamId"/> to be configured.</summary>
    public bool RequiresSteamId { get; set; }

    /// <summary>Whether this test additionally requires <see cref="TestConfig.TestTradeUrl"/> to be configured.</summary>
    public bool RequiresTradeUrl { get; set; }

    /// <inheritdoc />
    public override string? Skip
    {
        get
        {
            if (!TestConfig.HasApiKey)
                return "Set the 'SteamWebApi:ApiKey' user secret (or STEAMWEBAPI_API_KEY env var) to run live API tests.";
            if (RequiresSteamId && !TestConfig.HasTestSteamId)
                return "Set the 'SteamWebApi:TestSteamId' user secret (or STEAMWEBAPI_TEST_STEAMID env var) to run this test.";
            if (RequiresTradeUrl && !TestConfig.HasTestTradeUrl)
                return "Set the 'SteamWebApi:TestTradeUrl' user secret (or STEAMWEBAPI_TEST_TRADE_URL env var) to run this test.";
            return base.Skip;
        }
        set => base.Skip = value;
    }
}
