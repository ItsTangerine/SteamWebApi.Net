using System.Text.Json;
using SteamWebAPI.Models.Common;
using SteamWebAPI.Models.Info;
using SteamWebAPI.Results;

namespace SteamWebAPI;

public sealed partial class SteamWebApiClient
{
    /// <summary>
    /// Retrieves the structured item catalog (groups and the item types under each) used to build filters or
    /// autocomplete UIs. See <c>GET /steam/api/info/items</c> with <c>type=structed</c> (the default).
    /// </summary>
    /// <param name="game">The game to return metadata for. Defaults to CS2.</param>
    /// <param name="noCache">When true, bypasses steamwebapi.com's 24-hour cache for this call.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<IReadOnlyList<ItemInfoGroup>>> GetItemInfoStructureAsync(
        Game? game = null,
        bool? noCache = null,
        CancellationToken cancellationToken = default)
    {
        var query = NewQuery();
        SetEnumParam(query, "game", game);
        query["type"] = "structed";
        SetFlag(query, "no_cache", noCache);

        return await GetAsync<IReadOnlyList<ItemInfoGroup>>("/steam/api/info/items", query, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves a flat, deduplicated list of item groups, item types, or skin names.
    /// See <c>GET /steam/api/info/items</c> with <c>type=groups|types|items</c>.
    /// </summary>
    /// <param name="type">Which catalog of distinct values to return.</param>
    /// <param name="game">The game to return metadata for. Defaults to CS2.</param>
    /// <param name="noCache">When true, bypasses steamwebapi.com's 24-hour cache for this call.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<IReadOnlyList<string>>> GetItemInfoValuesAsync(
        ItemInfoValueType type,
        Game? game = null,
        bool? noCache = null,
        CancellationToken cancellationToken = default)
    {
        var query = NewQuery();
        SetEnumParam(query, "game", game);
        SetEnumParam(query, "type", (ItemInfoValueType?)type);
        SetFlag(query, "no_cache", noCache);

        return await GetAsync<IReadOnlyList<string>>("/steam/api/info/items", query, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Converts a SteamID between SteamID2, SteamID3, and SteamID64 formats. See <c>GET /steam/api/info/steamid</c>.
    /// </summary>
    /// <remarks>
    /// steamwebapi.com does not publish a response schema for this endpoint, so the payload is returned as a raw
    /// <see cref="JsonElement"/> rather than a typed model.
    /// </remarks>
    /// <param name="steamId">A SteamID in SteamID2 (<c>STEAM_0:0:...</c>), SteamID3 (<c>[U:1:...]</c>), or SteamID64 form.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<JsonElement>> ConvertSteamIdAsync(string steamId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(steamId))
            throw new ArgumentException("Steam ID must not be null or empty.", nameof(steamId));

        var query = NewQuery();
        query["steam_id"] = steamId;

        return await GetAsync<JsonElement>("/steam/api/info/steamid", query, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves all CS2/CS:GO containers (cases, sticker capsules, packages) matching the given filters.
    /// See <c>GET /steam/api/cs/containers</c>.
    /// </summary>
    /// <remarks>
    /// steamwebapi.com does not publish a response schema for this endpoint, so the payload is returned as a raw
    /// <see cref="JsonElement"/> rather than a typed model.
    /// </remarks>
    /// <param name="request">Container type filter, search, and sort options.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<JsonElement>> GetContainersAsync(
        GetContainersRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        request ??= new GetContainersRequest();
        var query = NewQuery();
        SetEnumParam(query, "type", (ContainerType?)request.Type);
        SetParam(query, "search", request.Search);
        SetEnumParam(query, "sortBy", request.SortBy);

        return await GetAsync<JsonElement>("/steam/api/cs/containers", query, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves a single CS2/CS:GO collection or case, with its items. See <c>GET /steam/api/cs/collection/{slug}</c>.
    /// </summary>
    /// <remarks>
    /// steamwebapi.com does not publish a response schema for this endpoint, so the payload is returned as a raw
    /// <see cref="JsonElement"/> rather than a typed model.
    /// </remarks>
    /// <param name="slug">The collection or case identifier.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<JsonElement>> GetCollectionAsync(string slug, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(slug))
            throw new ArgumentException("Slug must not be null or empty.", nameof(slug));

        var query = NewQuery();

        return await GetAsync<JsonElement>($"/steam/api/cs/collection/{Uri.EscapeDataString(slug)}", query, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves all CS2/CS:GO collections with their items and containers. See <c>GET /steam/api/cs/collections</c>.
    /// </summary>
    /// <remarks>
    /// steamwebapi.com does not publish a response schema for this endpoint, so the payload is returned as a raw
    /// <see cref="JsonElement"/> rather than a typed model.
    /// </remarks>
    /// <param name="request">Field selection and pagination options.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<JsonElement>> GetCollectionsAsync(
        GetCollectionsRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        request ??= new GetCollectionsRequest();
        var query = NewQuery();
        SetCsvParam(query, "select", request.SelectFields);
        SetParam(query, "limit", request.Limit);
        SetParam(query, "offset", request.Offset);
        SetFlag(query, "no_cache", request.NoCache);

        return await GetAsync<JsonElement>("/steam/api/cs/collections", query, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves the list of all third-party markets steamwebapi.com supports. See <c>GET /steam/api/info/markets</c>.
    /// </summary>
    /// <remarks>
    /// steamwebapi.com does not publish a response schema for this endpoint, so the payload is returned as a raw
    /// <see cref="JsonElement"/> rather than a typed model.
    /// </remarks>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<JsonElement>> GetSupportedMarketsAsync(CancellationToken cancellationToken = default)
    {
        var query = NewQuery();

        return await GetAsync<JsonElement>("/steam/api/info/markets", query, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Returns lightweight name+image autocomplete suggestions for a search-input field. Free tier; an API key is
    /// recommended for production use. See <c>GET /steam/api/complete/items</c>.
    /// </summary>
    /// <param name="search">The partial item name to autocomplete (minimum 3 characters).</param>
    /// <param name="game">The game to search, e.g. "cs2", "dota2", "rust". Defaults to "cs2".</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<IReadOnlyList<ItemAutocompleteSuggestion>>> AutocompleteItemsAsync(
        string search,
        string? game = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(search) || search.Length < 3)
            throw new ArgumentException("Search text must be at least 3 characters.", nameof(search));

        var query = NewQuery();
        query["search"] = search;
        SetParam(query, "game", game);

        return await GetAsync<IReadOnlyList<ItemAutocompleteSuggestion>>("/steam/api/complete/items", query, cancellationToken).ConfigureAwait(false);
    }
}
