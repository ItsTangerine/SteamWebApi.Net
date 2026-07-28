namespace SteamWebAPI.Tests;

/// <summary>
/// Documents the client methods that are intentionally NOT covered by live tests, and exactly why. Every stub
/// here is <c>[Fact(Skip = "...")]</c> rather than <see cref="Support.LiveFactAttribute"/>, because the reason
/// is not "no secret configured yet" (which would auto-resolve once a secret is added) but either:
/// <list type="bullet">
/// <item>a firm, permanent decision to never call state-mutating endpoints against a real Steam account
/// automatically (trade offer create/accept/cancel/decline, Steam Guard confirm/confirm-all/add/remove, and
/// the steamloginsecure login), or</item>
/// <item>a session/secret this test project's configuration genuinely does not provide (a live
/// <c>steamLoginSecure</c> cookie, a Steam Guard <c>shared_secret</c>, or an <c>identity_secret</c> — see
/// <see cref="Support.TestConfig"/>, which only exposes an API key, a test SteamID, and an optional trade URL).</item>
/// </list>
/// These stubs exist purely so the exclusion is visible in test output and the IDE test explorer, rather than
/// the endpoint being silently absent from the test suite altogether. Bodies are never executed.
/// </summary>
public class ExcludedEndpointsTests
{
    // ---- SteamWebApiClient.SteamGuard.cs (7 methods) ----

    [Fact(Skip = "Requires a live Steam Guard shared_secret (TOTP secret) to generate a real code. This test " +
                 "project's secrets provide only an API key, test SteamID, and optional trade URL (see TestConfig) " +
                 "- no shared_secret is configured, so this is excluded until one is added.")]
    public void GenerateGuardCodeAsync_Excluded()
    {
    }

    [Fact(Skip = "Lists real pending mobile trade/market confirmations, which requires a live steamLoginSecure " +
                 "session, identity_secret, and SteamId. None of these are available in this test project's " +
                 "configured secrets (only an API key, test SteamID, and optional trade URL); excluded until " +
                 "session secrets are added.")]
    public void ListConfirmationsAsync_Excluded()
    {
    }

    [Fact(Skip = "Accepts or denies real pending mobile confirmations (e.g. trades) on a live Steam account, and " +
                 "additionally requires a live steamLoginSecure session and identity_secret this test project's " +
                 "secrets don't provide; excluded from automated testing per user decision - never call this " +
                 "against a real account automatically.")]
    public void ConfirmConfirmationsAsync_Excluded()
    {
    }

    [Fact(Skip = "Fetches the detail view of a single real pending confirmation (mobileconf/details), which " +
                 "requires a live steamLoginSecure session, identity_secret, and a real ConfId. None of these are " +
                 "available in this test project's configured secrets (only an API key, test SteamID, and optional " +
                 "trade URL); excluded until session secrets are added.")]
    public void GetConfirmationDetailsAsync_Excluded()
    {
    }

    [Fact(Skip = "Bulk-accepts or denies real pending mobile confirmations on a live Steam account in a single " +
                 "call, and additionally requires a live steamLoginSecure session and identity_secret this test " +
                 "project's secrets don't provide; excluded from automated testing per user decision - never call " +
                 "this against a real account automatically.")]
    public void ConfirmAllConfirmationsAsync_Excluded()
    {
    }

    [Fact(Skip = "Enrolls (adds) a mobile Steam Guard authenticator on a real Steam account - covers both " +
                 "AddGuardAsync and its raw-maFile-download sibling AddGuardDownloadMaFileAsync, which share the " +
                 "same POST /steam/api/guard/add endpoint and lifecycle. Requires live account username/password " +
                 "and step-specific codes this test project's secrets don't provide; excluded from automated " +
                 "testing per user decision - never call this against a real account automatically.")]
    public void AddGuardAsync_Excluded()
    {
    }

    [Fact(Skip = "Deactivates (removes) the mobile Steam Guard authenticator on a real Steam account, reverting it " +
                 "to email Steam Guard. Requires live account username/password, the current shared_secret, and a " +
                 "revocation code this test project's secrets don't provide; excluded from automated testing per " +
                 "user decision - never call this against a real account automatically.")]
    public void RemoveGuardAsync_Excluded()
    {
    }

    // ---- SteamWebApiClient.TradeOffers.cs (9 methods) ----

    [Fact(Skip = "Creates a real trade offer on a live Steam account via an authenticated steamLoginSecure " +
                 "session, which this test project's secrets don't provide; excluded from automated testing - " +
                 "never call this against a real account automatically.")]
    public void CreateTradeOfferAsync_Excluded()
    {
    }

    [Fact(Skip = "Accepts a real trade offer on a live Steam account (may transfer items and require a mobile " +
                 "authenticator confirmation) via an authenticated steamLoginSecure session, which this test " +
                 "project's secrets don't provide; excluded from automated testing - never call this against a " +
                 "real account automatically.")]
    public void AcceptTradeOfferAsync_Excluded()
    {
    }

    [Fact(Skip = "Requires a live steamLoginSecure session belonging to the offer's recipient, which isn't " +
                 "available in this test project's configured secrets (only an API key, test SteamID, and optional " +
                 "trade URL); excluded until session secrets are added.")]
    public void CheckTradeOfferAsync_Excluded()
    {
    }

    [Fact(Skip = "Requires a live steamLoginSecure session to page through Steam's IEconService/GetTradeHistory, " +
                 "which isn't available in this test project's configured secrets (only an API key, test SteamID, " +
                 "and optional trade URL); excluded until session secrets are added.")]
    public void GetTradeHistoryAsync_Excluded()
    {
    }

    [Fact(Skip = "Requires a live steamLoginSecure session to list an account's outgoing trade offers, which isn't " +
                 "available in this test project's configured secrets (only an API key, test SteamID, and optional " +
                 "trade URL); excluded until session secrets are added.")]
    public void GetSentTradeOffersAsync_Excluded()
    {
    }

    [Fact(Skip = "Requires a live steamLoginSecure session to list an account's incoming trade offers, which isn't " +
                 "available in this test project's configured secrets (only an API key, test SteamID, and optional " +
                 "trade URL); excluded until session secrets are added.")]
    public void GetPendingTradeOffersAsync_Excluded()
    {
    }

    [Fact(Skip = "Requires a live steamLoginSecure session to list an account's historical outgoing trade offers, " +
                 "which isn't available in this test project's configured secrets (only an API key, test SteamID, " +
                 "and optional trade URL); excluded until session secrets are added.")]
    public void GetSentTradeOfferHistoryAsync_Excluded()
    {
    }

    [Fact(Skip = "Cancels a real trade offer (sender-initiated) on a live Steam account via an authenticated " +
                 "steamLoginSecure session, which this test project's secrets don't provide; excluded from " +
                 "automated testing - never call this against a real account automatically.")]
    public void CancelTradeOfferAsync_Excluded()
    {
    }

    [Fact(Skip = "Declines a real trade offer (recipient-initiated) on a live Steam account via an authenticated " +
                 "steamLoginSecure session, which this test project's secrets don't provide; excluded from " +
                 "automated testing - never call this against a real account automatically.")]
    public void DeclineTradeOfferAsync_Excluded()
    {
    }

    // ---- SteamWebApiClient.Account.cs (1 method: steamloginsecure login) ----

    [Fact(Skip = "Performs a real Steam login (username/password/Guard code, or a refresh token) against a live " +
                 "Steam account to obtain steamLoginSecure session cookies. This test project's secrets provide " +
                 "only an API key, test SteamID, and optional trade URL - no login credentials or refresh token; " +
                 "excluded from automated testing per user decision - never call this against a real account " +
                 "automatically.")]
    public void SteamLoginSecureAsync_Excluded()
    {
    }
}
