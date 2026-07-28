using System.Text.Json;
using SteamWebAPI.Models.Common;
using SteamWebAPI.Models.Items;
using SteamWebAPI.Results;

namespace SteamWebAPI;

public sealed partial class SteamWebApiClient
{
    /// <summary>
    /// Retrieves the full priced item catalog for a game, with filtering, sorting, and pagination.
    /// See <c>GET /steam/api/items</c>.
    /// </summary>
    /// <param name="request">Filtering, sorting, and pagination options. When omitted, returns CS2 items sorted by ascending Steam price.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<IReadOnlyList<SteamItem>>> GetItemsAsync(
        GetItemsRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        request ??= new GetItemsRequest();
        var query = NewQuery();
        SetEnumParam(query, "game", request.Game);
        SetParam(query, "page", request.Page);
        SetParam(query, "max", request.Max);
        SetEnumParam(query, "sort_by", request.SortBy);
        SetParam(query, "search", request.Search);
        SetParam(query, "price_min", request.PriceMin);
        SetParam(query, "price_max", request.PriceMax);
        SetParam(query, "price_real_min", request.PriceRealMin);
        SetParam(query, "price_real_max", request.PriceRealMax);
        SetCsvParam(query, "item_group", request.ItemGroups);
        SetParam(query, "item_type", request.ItemType);
        SetParam(query, "item_name", request.ItemName);
        SetCsvEnumParam(query, "wear", request.Wears);
        SetCsvParam(query, "select", request.SelectFields);
        SetEnumParam(query, "currency", request.Currency);
        SetCsvEnumParam(query, "markets", request.Markets);
        SetFlag(query, "with_preview_items", request.WithPreviewItems);
        query["production"] = "1";

        return await GetAsync<IReadOnlyList<SteamItem>>("/steam/api/items", query, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves enriched details (pricing, tags, brief history) for a single item by its market hash name.
    /// See <c>GET /steam/api/item</c>.
    /// </summary>
    /// <param name="marketHashName">The item's Steam Market hash name, e.g. "AK-47 | Redline (Field-Tested)".</param>
    /// <param name="request">Additional lookup options.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<SteamItem>> GetItemAsync(
        string marketHashName,
        GetItemRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(marketHashName))
            throw new ArgumentException("Market hash name must not be null or empty.", nameof(marketHashName));

        request ??= new GetItemRequest();
        var query = NewQuery();
        query["market_hash_name"] = marketHashName;
        SetEnumParam(query, "currency", request.Currency);
        SetFlag(query, "with_groups", request.WithGroups);
        SetCsvEnumParam(query, "markets", request.Markets);
        query["production"] = "1";

        return await GetAsync<SteamItem>("/steam/api/item", query, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves the daily price history for a single item. See <c>GET /steam/api/history</c>.
    /// </summary>
    /// <param name="marketHashName">The item's Steam Market hash name.</param>
    /// <param name="request">History source and date-range options.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<IReadOnlyList<ItemPriceHistoryEntry>>> GetItemPriceHistoryAsync(
        string marketHashName,
        GetItemPriceHistoryRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(marketHashName))
            throw new ArgumentException("Market hash name must not be null or empty.", nameof(marketHashName));

        request ??= new GetItemPriceHistoryRequest();
        var query = NewQuery();
        query["market_hash_name"] = marketHashName;
        SetEnumParam(query, "origin", request.Origin);
        SetParam(query, "interval", request.IntervalDays);
        SetDateParam(query, "start_date", request.StartDate);
        SetDateParam(query, "end_date", request.EndDate);
        query["production"] = "1";

        return await GetAsync<IReadOnlyList<ItemPriceHistoryEntry>>("/steam/api/history", query, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves daily aggregated worth/count history for a set of items in a single call (1 credit total).
    /// See <c>POST /steam/api/items/history</c>.
    /// </summary>
    /// <param name="request">The items to aggregate and the aggregation options.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<IReadOnlyList<AggregatedHistoryEntry>>> GetItemsAggregatedHistoryAsync(
        GetItemsAggregatedHistoryRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (request.Items is null || request.Items.Count == 0)
            throw new ArgumentException("At least one item must be specified.", nameof(request));

        var query = NewQuery();

        return await PostAsync<IReadOnlyList<AggregatedHistoryEntry>>("/steam/api/items/history", query, request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves lightweight CS2 item metadata (no pricing), for building filters or discovering new items.
    /// See <c>GET /steam/api/items/preview</c>.
    /// </summary>
    /// <param name="request">Search/filter options.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<IReadOnlyList<ItemPreview>>> GetItemsPreviewAsync(
        GetItemsPreviewRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        request ??= new GetItemsPreviewRequest();
        var query = NewQuery();
        SetParam(query, "search", request.Search);
        SetParam(query, "paint_index", request.PaintIndex);
        SetParam(query, "def_index", request.DefIndex);
        SetFlag(query, "grouped", request.Grouped);
        SetFlag(query, "show_all", request.ShowAll);
        SetFlag(query, "preview", request.PreviewOnly);
        SetFlag(query, "only_phases", request.OnlyPhases);

        return await GetAsync<IReadOnlyList<ItemPreview>>("/steam/api/items/preview", query, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves the distinct set of values for one metadata field across the CS2 item catalog (e.g. all known
    /// item types, or all known rarities). See <c>GET /steam/api/items/preview</c> with <c>groupBy</c> set.
    /// </summary>
    /// <param name="groupBy">The field to enumerate distinct values for.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<IReadOnlyList<string>>> GetItemsPreviewGroupValuesAsync(
        PreviewGroupBy groupBy,
        CancellationToken cancellationToken = default)
    {
        var query = NewQuery();
        SetEnumParam(query, "groupBy", (PreviewGroupBy?)groupBy);

        return await GetAsync<IReadOnlyList<string>>("/steam/api/items/preview", query, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves realtime Steam buy/sell order-book activity for a single item.
    /// See <c>GET /steam/api/itemordersactivity</c>.
    /// </summary>
    /// <remarks>
    /// steamwebapi.com does not publish a response schema for this endpoint, so the payload is returned as a raw
    /// <see cref="JsonElement"/> rather than a typed model.
    /// </remarks>
    /// <param name="marketHashName">The item's Steam Market hash name.</param>
    /// <param name="request">Regional/currency options.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<JsonElement>> GetItemOrderActivityAsync(
        string marketHashName,
        GetItemOrderActivityRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(marketHashName))
            throw new ArgumentException("Market hash name must not be null or empty.", nameof(marketHashName));

        request ??= new GetItemOrderActivityRequest();
        var query = NewQuery();
        query["market_hash_name"] = marketHashName;
        SetParam(query, "country", request.Country);
        SetParam(query, "language", request.Language);
        SetParam(query, "currency", request.Currency);
        query["production"] = "1";

        return await GetAsync<JsonElement>("/steam/api/itemordersactivity", query, cancellationToken).ConfigureAwait(false);
    }
}
