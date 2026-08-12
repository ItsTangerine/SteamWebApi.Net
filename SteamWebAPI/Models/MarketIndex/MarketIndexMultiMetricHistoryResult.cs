using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.MarketIndex;

/// <summary>
/// Multiple metrics' time series in one payload, as returned by
/// <see cref="SteamWebApiClient.GetMarketIndexMultiMetricHistoryAsync"/> (HTTP 202).
/// </summary>
public sealed class MarketIndexMultiMetricHistoryResult : BaseResponseDto
{
    /// <summary>Whether the request succeeded.</summary>
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    /// <summary>Always "multi-metric" for this response shape.</summary>
    [JsonPropertyName("mode")]
    public string? Mode { get; set; }

    /// <summary>The metrics this history covers.</summary>
    [JsonPropertyName("metrics")]
    public IReadOnlyList<string>? Metrics { get; set; }

    /// <summary>The aggregation interval used.</summary>
    [JsonPropertyName("interval")]
    public string? Interval { get; set; }

    /// <summary>The segmentation axis used.</summary>
    [JsonPropertyName("segmenttype")]
    public string? SegmentType { get; set; }

    /// <summary>The specific segment value used.</summary>
    [JsonPropertyName("segmentkey")]
    public string? SegmentKey { get; set; }

    /// <summary>The start of the covered time range.</summary>
    [JsonPropertyName("from")]
    public string? From { get; set; }

    /// <summary>The end of the covered time range.</summary>
    [JsonPropertyName("to")]
    public string? To { get; set; }

    /// <summary>The time series data points, keyed by metric name.</summary>
    [JsonPropertyName("history")]
    public IReadOnlyDictionary<string, IReadOnlyList<HistoryPointShort>>? History { get; set; }
}

/// <summary>
/// A single OHLC time series point in multi-metric mode, embedded in a <see cref="MarketIndexMultiMetricHistoryResult"/>.
/// Unlike <see cref="HistoryPoint"/>, this does not carry <c>change</c>/<c>trend</c>/<c>datapoints</c>, per
/// steamwebapi.com's multi-metric example.
/// </summary>
public sealed class HistoryPointShort : BaseResponseDto
{
    /// <summary>The point's timestamp, as a Unix timestamp in seconds.</summary>
    [JsonPropertyName("ts")]
    public long Ts { get; set; }

    /// <summary>The opening value for this interval.</summary>
    [JsonPropertyName("open")]
    public decimal? Open { get; set; }

    /// <summary>The highest value observed during this interval.</summary>
    [JsonPropertyName("high")]
    public decimal? High { get; set; }

    /// <summary>The lowest value observed during this interval.</summary>
    [JsonPropertyName("low")]
    public decimal? Low { get; set; }

    /// <summary>The closing value for this interval.</summary>
    [JsonPropertyName("close")]
    public decimal? Close { get; set; }
}
