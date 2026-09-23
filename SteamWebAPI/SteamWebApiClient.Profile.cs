using SteamWebAPI.Models.Profile;
using SteamWebAPI.Results;

namespace SteamWebAPI;

public sealed partial class SteamWebApiClient
{
    /// <summary>
    /// Fetches a Steam user's friends list. See <c>GET /steam/api/friendlist</c>.
    /// </summary>
    /// <param name="id">The friend list owner's SteamID64 or vanity URL. Required.</param>
    /// <param name="noCache">When true, bypasses steamwebapi.com's 1-day cache for this call.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<IReadOnlyList<FriendListEntry>>> GetFriendListAsync(
        string id,
        bool? noCache = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("Steam ID or vanity URL must not be null or empty.", nameof(id));

        var query = NewQuery();
        query["id"] = id;
        SetFlag(query, "no_cache", noCache);
        query["production"] = "1";

        return await GetAsync<IReadOnlyList<FriendListEntry>>("/steam/api/friendlist", query, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Fetches a single Steam user profile. See <c>GET /steam/api/profile</c>.
    /// </summary>
    /// <param name="id">The Steam profile's SteamID, profile URL, or username. Required.</param>
    /// <param name="request">Depth and caching options.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<SteamProfile>> GetProfileAsync(
        string id,
        GetProfileRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("Steam ID, profile URL, or username must not be null or empty.", nameof(id));

        request ??= new GetProfileRequest();
        var query = NewQuery();
        query["id"] = id;
        SetFlag(query, "no_cache", request.NoCache);
        SetEnumParam(query, "state", request.State);
        SetFlag(query, "force_from_db_if_exists", request.ForceFromDbIfExists);
        SetFlag(query, "with_groups", request.WithGroups);
        query["production"] = "1";

        return await GetAsync<SteamProfile>("/steam/api/profile", query, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Fetches up to 20 Steam user profiles in a single call (1 credit each). See <c>GET /steam/api/profile/batch</c>.
    /// </summary>
    /// <param name="steamIds">The SteamIDs to fetch. Required; at most 20.</param>
    /// <param name="request">Depth options.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<ProfileBatchResult>> GetProfileBatchAsync(
        IReadOnlyList<string> steamIds,
        GetProfileBatchRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        if (steamIds is null || steamIds.Count == 0)
            throw new ArgumentException("At least one Steam ID must be specified.", nameof(steamIds));
        if (steamIds.Count > 20)
            throw new ArgumentException("At most 20 Steam IDs may be requested in a single batch call.", nameof(steamIds));

        request ??= new GetProfileBatchRequest();
        var query = NewQuery();
        SetCsvParam(query, "id", steamIds);
        SetEnumParam(query, "state", request.State);
        SetFlag(query, "with_groups", request.WithGroups);

        return await GetAsync<ProfileBatchResult>("/steam/api/profile/batch", query, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Lists every game a Steam account owns, with all-time playtime and achievement progress, plus the account's
    /// games-page counters. Costs 1 credit (2 when <paramref name="noCache"/> is true); 404 and 503 responses are
    /// free. See <c>GET /steam/api/profile/games</c>.
    /// </summary>
    /// <param name="id">The account's SteamID64, vanity name, or full profile URL. Required.</param>
    /// <param name="noCache">When true, bypasses steamwebapi.com's 5-minute cache for this call.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<ProfileGamesResult>> GetProfileGamesAsync(
        string id,
        bool? noCache = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("Steam ID, vanity name, or profile URL must not be null or empty.", nameof(id));

        var query = NewQuery();
        query["id"] = id;
        SetFlag(query, "no_cache", noCache);
        query["production"] = "1";

        return await GetAsync<ProfileGamesResult>("/steam/api/profile/games", query, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Scores how likely a Steam account is a secondary ("smurf") account whose inventory was handed over from another
    /// account. A probabilistic indicator, not proof: don't use it as the sole basis for banning or rejecting a user.
    /// Costs 1 credit. See <c>GET /steam/api/profile/risk</c>.
    /// </summary>
    /// <param name="steamId">The account's SteamID64. Required.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<ProfileRiskResult>> GetProfileRiskAsync(
        string steamId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(steamId))
            throw new ArgumentException("Steam ID must not be null or empty.", nameof(steamId));

        var query = NewQuery();
        query["steam_id"] = steamId;

        return await GetAsync<ProfileRiskResult>("/steam/api/profile/risk", query, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves the observed trade history of a Steam account (every CS2 item handover seen, in both directions),
    /// plus a per-counterpart roll-up. Costs 1 credit per started block of 50 returned rows.
    /// See <c>GET /steam/api/profile/trades</c>.
    /// </summary>
    /// <param name="steamId">The account's SteamID64. Required.</param>
    /// <param name="request">Filtering, sorting, and pagination options.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<ProfileTradesResult>> GetProfileTradesAsync(
        string steamId,
        GetProfileTradesRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(steamId))
            throw new ArgumentException("Steam ID must not be null or empty.", nameof(steamId));

        request ??= new GetProfileTradesRequest();
        var query = NewQuery();
        query["steam_id"] = steamId;
        SetEnumParam(query, "direction", request.Direction);
        SetParam(query, "partner", request.Partner);
        SetDateParam(query, "from", request.From);
        SetDateParam(query, "to", request.To);
        SetEnumParam(query, "sort", request.Sort);
        SetParam(query, "limit", request.Limit);
        SetParam(query, "offset", request.Offset);

        return await GetAsync<ProfileTradesResult>("/steam/api/profile/trades", query, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Validates a Steam trade URL and reports its escrow/trade-hold status. See <c>GET /steam/api/profile/trade-eligibility</c>.
    /// </summary>
    /// <param name="tradeUrl">The Steam trade URL to validate. Required.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<TradeEligibilityResult>> GetTradeEligibilityAsync(
        string tradeUrl,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(tradeUrl))
            throw new ArgumentException("Trade URL must not be null or empty.", nameof(tradeUrl));

        var query = NewQuery();
        query["trade_url"] = tradeUrl;

        return await GetAsync<TradeEligibilityResult>("/steam/api/profile/trade-eligibility", query, cancellationToken).ConfigureAwait(false);
    }
}
