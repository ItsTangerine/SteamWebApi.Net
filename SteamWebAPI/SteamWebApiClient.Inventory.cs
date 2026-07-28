using System.Text.Json;
using SteamWebAPI.Models.Inventory;
using SteamWebAPI.Results;

namespace SteamWebAPI;

public sealed partial class SteamWebApiClient
{
    /// <summary>
    /// Fetches a Steam inventory enriched with live prices, float values, stickers, and Doppler phase detection.
    /// See <c>GET /steam/api/inventory</c> with <c>parse=1</c> (the default and only mode this method returns
    /// typed results for; see <see cref="GetInventoryRawAsync"/> for the undocumented <c>parse=0</c> raw mode).
    /// </summary>
    /// <param name="steamId">
    /// The inventory owner's steamid/steamid3/steamid64/vanity URL. Required unless
    /// <see cref="GetInventoryRequest.SteamLoginSecure"/> is set, in which case it is ignored server-side (pass any
    /// non-empty placeholder).
    /// </param>
    /// <param name="request">Filtering, pricing, and pagination options.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<IReadOnlyList<InventoryItem>>> GetInventoryAsync(
        string steamId,
        GetInventoryRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(steamId))
            throw new ArgumentException("Steam ID must not be null or empty.", nameof(steamId));

        request ??= new GetInventoryRequest();
        var query = NewQuery();
        query["steam_id"] = steamId;
        SetEnumParam(query, "state", request.State);
        SetParam(query, "steam_login_secure", request.SteamLoginSecure);
        SetParam(query, "game", request.Game);
        query["parse"] = "1";
        SetEnumParam(query, "language", request.Language);
        SetFlag(query, "no_cache", request.NoCache);
        SetFlag(query, "group", request.Group);
        SetEnumParam(query, "sort", request.Sort);
        SetEnumParam(query, "currency", request.Currency);
        SetCsvParam(query, "select", request.SelectFields);
        SetFlag(query, "with_no_tradable", request.WithNoTradable);
        SetParam(query, "trade_url", request.TradeUrl);
        SetParam(query, "offset", request.Offset);
        SetParam(query, "limit", request.Limit);
        SetFlag(query, "try_first_seven_days_blocked_items", request.TryFirstSevenDaysBlockedItems);
        SetCsvEnumParam(query, "markets", request.Markets);
        SetFlag(query, "with_prices", request.WithPrices);
        SetFlag(query, "with_phase_price", request.WithPhasePrice);
        SetParam(query, "search", request.Search);
        query["production"] = "1";
        SetFlag(query, "trade_locked", request.TradeLocked);
        SetParam(query, "start_assetid", request.StartAssetId);

        return await GetAsync<IReadOnlyList<InventoryItem>>("/steam/api/inventory", query, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Fetches a Steam inventory in Steam's raw, undocumented native JSON shape (<c>assets</c>/<c>descriptions</c>),
    /// bypassing steamwebapi.com's enrichment. See <c>GET /steam/api/inventory</c> with <c>parse=0</c>.
    /// </summary>
    /// <remarks>
    /// steamwebapi.com does not publish a schema for the raw Steam inventory format, so the payload is returned as
    /// a raw <see cref="JsonElement"/> rather than a typed model.
    /// </remarks>
    /// <param name="steamId">The inventory owner's steamid/steamid3/steamid64/vanity URL. Required.</param>
    /// <param name="game">The game to fetch the inventory for. Free text; defaults to "cs2".</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<JsonElement>> GetInventoryRawAsync(
        string steamId,
        string? game = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(steamId))
            throw new ArgumentException("Steam ID must not be null or empty.", nameof(steamId));

        var query = NewQuery();
        query["steam_id"] = steamId;
        SetParam(query, "game", game);
        query["parse"] = "0";
        query["production"] = "1";

        return await GetAsync<JsonElement>("/steam/api/inventory", query, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Fetches enriched inventories for up to 20 Steam IDs in a single call (1 credit each), parallelized.
    /// See <c>GET /steam/api/inventory/batch</c>.
    /// </summary>
    /// <param name="steamIds">The inventory owners' SteamIDs. Required; at most 20.</param>
    /// <param name="request">Filtering and pricing options.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A dictionary keyed by SteamID64 (as a string), each value the matching items for that Steam ID.</returns>
    public async Task<Result<Dictionary<string, List<InventoryItem>>>> GetInventoryBatchAsync(
        IReadOnlyList<string> steamIds,
        GetInventoryBatchRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        if (steamIds is null || steamIds.Count == 0)
            throw new ArgumentException("At least one Steam ID must be specified.", nameof(steamIds));
        if (steamIds.Count > 20)
            throw new ArgumentException("At most 20 Steam IDs may be requested in a single batch call.", nameof(steamIds));

        request ??= new GetInventoryBatchRequest();
        var query = NewQuery();
        SetCsvParam(query, "steam_ids", steamIds);
        SetEnumParam(query, "game", request.Game);
        SetCsvParam(query, "select", request.SelectFields);
        SetEnumParam(query, "currency", request.Currency);
        SetEnumParam(query, "language", request.Language);
        SetFlag(query, "no_cache", request.NoCache);
        SetFlag(query, "with_no_tradable", request.WithNoTradable);
        SetFlag(query, "with_phase_price", request.WithPhasePrice);
        SetEnumParam(query, "sort", request.Sort);
        SetFlag(query, "group", request.Group);
        SetCsvEnumParam(query, "markets", request.Markets);
        query["production"] = "1";

        return await GetAsync<Dictionary<string, List<InventoryItem>>>("/steam/api/inventory/batch", query, cancellationToken).ConfigureAwait(false);
    }
}
