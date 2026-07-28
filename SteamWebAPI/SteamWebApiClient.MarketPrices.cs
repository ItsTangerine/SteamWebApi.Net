using SteamWebAPI.Models.MarketPrices;
using SteamWebAPI.Results;

namespace SteamWebAPI;

public sealed partial class SteamWebApiClient
{
    /// <summary>
    /// Retrieves the latest prices for all (or one) items on a single named third-party market. This is a premium
    /// endpoint subject to the same rate limits as <c>GET /steam/api/items</c>. See <c>GET /market/{market}/prices</c>.
    /// </summary>
    /// <param name="marketIdent">
    /// The market ident, e.g. "buff", "csfloat", "youpin", "skinport". This is open-ended (not an exhaustive closed
    /// set), so it is a plain string rather than the <see cref="Models.Common.Market"/> enum.
    /// </param>
    /// <param name="request">Item filter and currency options.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<IReadOnlyList<MarketPriceListing>>> GetMarketPricesAsync(
        string marketIdent,
        GetMarketPricesRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(marketIdent))
            throw new ArgumentException("Market ident must not be null or empty.", nameof(marketIdent));

        request ??= new GetMarketPricesRequest();
        var query = NewQuery();
        SetParam(query, "market_hash_name", request.MarketHashName);
        SetEnumParam(query, "currency", request.Currency);

        return await GetAsync<IReadOnlyList<MarketPriceListing>>($"/market/{Uri.EscapeDataString(marketIdent)}/prices", query, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves the latest prices for one or all items across all configured third-party markets in a single
    /// payload, grouped per item. See <c>GET /markets/prices</c>.
    /// </summary>
    /// <param name="request">Item filter, market allowlist, and currency options.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<IReadOnlyList<ItemMarketPrices>>> GetMarketsPricesAsync(
        GetMarketsPricesRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        request ??= new GetMarketsPricesRequest();
        var query = NewQuery();
        SetParam(query, "market_hash_name", request.MarketHashName);
        SetCsvParam(query, "markets", request.Markets);
        SetEnumParam(query, "currency", request.Currency);

        return await GetAsync<IReadOnlyList<ItemMarketPrices>>("/markets/prices", query, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves the daily price history for a single item across all configured third-party markets in one
    /// payload. Costs 2 credits per request. See <c>GET /markets/history</c>.
    /// </summary>
    /// <param name="marketHashName">The item's Steam Market hash name.</param>
    /// <param name="request">Market allowlist, date-range, and currency options.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<ItemMarketHistory>> GetMarketsHistoryAsync(
        string marketHashName,
        GetMarketsHistoryRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(marketHashName))
            throw new ArgumentException("Market hash name must not be null or empty.", nameof(marketHashName));

        request ??= new GetMarketsHistoryRequest();
        var query = NewQuery();
        query["market_hash_name"] = marketHashName;
        SetCsvParam(query, "markets", request.Markets);
        SetDateParam(query, "start_date", request.StartDate);
        SetDateParam(query, "end_date", request.EndDate);
        SetEnumParam(query, "currency", request.Currency);

        return await GetAsync<ItemMarketHistory>("/markets/history", query, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves the daily price history for a single item on a single named third-party market. Costs 2 credits
    /// per request. See <c>GET /market/{market}/history</c>.
    /// </summary>
    /// <param name="marketIdent">
    /// The market ident, e.g. "buff", "csfloat", "youpin", "skinport". This is open-ended (not an exhaustive closed
    /// set), so it is a plain string rather than the <see cref="Models.Common.Market"/> enum.
    /// </param>
    /// <param name="marketHashName">The item's Steam Market hash name.</param>
    /// <param name="request">Date-range and currency options.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<IReadOnlyList<MarketHistoryPoint>>> GetMarketHistoryAsync(
        string marketIdent,
        string marketHashName,
        GetMarketHistoryRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(marketIdent))
            throw new ArgumentException("Market ident must not be null or empty.", nameof(marketIdent));
        if (string.IsNullOrWhiteSpace(marketHashName))
            throw new ArgumentException("Market hash name must not be null or empty.", nameof(marketHashName));

        request ??= new GetMarketHistoryRequest();
        var query = NewQuery();
        query["market_hash_name"] = marketHashName;
        SetDateParam(query, "start_date", request.StartDate);
        SetDateParam(query, "end_date", request.EndDate);
        SetEnumParam(query, "currency", request.Currency);

        return await GetAsync<IReadOnlyList<MarketHistoryPoint>>($"/market/{Uri.EscapeDataString(marketIdent)}/history", query, cancellationToken).ConfigureAwait(false);
    }
}
