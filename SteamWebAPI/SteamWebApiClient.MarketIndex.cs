using SteamWebAPI.Models.MarketIndex;
using SteamWebAPI.Results;

namespace SteamWebAPI;

public sealed partial class SteamWebApiClient
{
    /// <summary>
    /// Retrieves the CS2 global market index (all segments), or a single segment's stats.
    /// See <c>GET /steam/api/market-index/cs2</c>.
    /// </summary>
    /// <param name="request">Segment filter options. When omitted, the global overview is returned.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<MarketIndexResult>> GetMarketIndexAsync(
        GetMarketIndexRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        request ??= new GetMarketIndexRequest();
        if (request.SegmentType is not null && string.IsNullOrWhiteSpace(request.SegmentKey))
            throw new ArgumentException("SegmentKey is required when SegmentType is set.", nameof(request));

        var query = NewQuery();
        SetEnumParam(query, "segment_type", request.SegmentType);
        SetParam(query, "segment_key", request.SegmentKey);

        return await GetAsync<MarketIndexResult>("/steam/api/market-index/cs2", query, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves the historical time series for a single market index metric, raw or OHLC-aggregated.
    /// See <c>GET /steam/api/market-index/cs2/history</c> (single-metric mode, HTTP 200).
    /// </summary>
    /// <param name="request">Metric, interval, segmentation, and date-range options.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<MarketIndexHistoryResult>> GetMarketIndexHistoryAsync(
        GetMarketIndexHistoryRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        request ??= new GetMarketIndexHistoryRequest();
        var query = NewQuery();
        SetEnumParam(query, "metric", request.Metric);
        SetEnumParam(query, "interval", request.Interval);
        SetEnumParam(query, "segment_type", request.SegmentType);
        SetParam(query, "segment_key", request.SegmentKey);
        SetParam(query, "from", request.From);
        SetParam(query, "to", request.To);
        SetParam(query, "limit", request.Limit);

        return await GetAsync<MarketIndexHistoryResult>("/steam/api/market-index/cs2/history", query, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves the historical time series for several market index metrics in one payload, raw or OHLC-aggregated.
    /// See <c>GET /steam/api/market-index/cs2/history</c> with <c>metrics</c> set (multi-metric mode, HTTP 202).
    /// </summary>
    /// <param name="metrics">The metrics to return history for. At least one is required.</param>
    /// <param name="request">Interval, segmentation, and date-range options.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<MarketIndexMultiMetricHistoryResult>> GetMarketIndexMultiMetricHistoryAsync(
        IReadOnlyList<MarketIndexMetric> metrics,
        GetMarketIndexMultiMetricHistoryRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        if (metrics is null || metrics.Count == 0)
            throw new ArgumentException("At least one metric must be specified.", nameof(metrics));

        request ??= new GetMarketIndexMultiMetricHistoryRequest();
        var query = NewQuery();
        SetCsvEnumParam(query, "metrics", metrics);
        SetEnumParam(query, "interval", request.Interval);
        SetEnumParam(query, "segment_type", request.SegmentType);
        SetParam(query, "segment_key", request.SegmentKey);
        SetParam(query, "from", request.From);
        SetParam(query, "to", request.To);
        SetParam(query, "limit", request.Limit);

        return await GetAsync<MarketIndexMultiMetricHistoryResult>("/steam/api/market-index/cs2/history", query, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Compares all (or a filtered subset of) segments of a given type by a chosen metric, sorted descending.
    /// See <c>GET /steam/api/market-index/cs2/compare</c>.
    /// </summary>
    /// <param name="type">The segmentation axis to compare.</param>
    /// <param name="request">Segment allowlist and ranking metric options.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<MarketIndexCompareResult>> GetMarketIndexCompareAsync(
        MarketIndexSegmentType type,
        GetMarketIndexCompareRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        request ??= new GetMarketIndexCompareRequest();
        var query = NewQuery();
        SetEnumParam(query, "type", (MarketIndexSegmentType?)type);
        SetCsvParam(query, "keys", request.Keys);
        SetEnumParam(query, "metric", request.Metric);

        return await GetAsync<MarketIndexCompareResult>("/steam/api/market-index/cs2/compare", query, cancellationToken).ConfigureAwait(false);
    }
}
