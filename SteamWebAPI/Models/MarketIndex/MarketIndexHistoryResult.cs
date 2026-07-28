using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.MarketIndex;

/// <summary>
/// A single metric's time series, as returned by <see cref="SteamWebApiClient.GetMarketIndexHistoryAsync"/> (HTTP 200).
/// </summary>
public sealed class MarketIndexHistoryResult
{
    /// <summary>Whether the request succeeded.</summary>
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    /// <summary>The metric this history covers.</summary>
    [JsonPropertyName("metric")]
    public string? Metric { get; set; }

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

    /// <summary>The number of points in <see cref="History"/>.</summary>
    [JsonPropertyName("count")]
    public int? Count { get; set; }

    /// <summary>The full set of metric names that can be requested.</summary>
    [JsonPropertyName("availablemetrics")]
    public IReadOnlyList<string>? AvailableMetrics { get; set; }

    /// <summary>The full set of interval names that can be requested.</summary>
    [JsonPropertyName("availableintervals")]
    public IReadOnlyList<string>? AvailableIntervals { get; set; }

    /// <summary>The time series data points.</summary>
    [JsonPropertyName("history")]
    public IReadOnlyList<HistoryPoint>? History { get; set; }
}

/// <summary>A single OHLC (or raw-value) time series point, embedded in a <see cref="MarketIndexHistoryResult"/>.</summary>
public sealed class HistoryPoint
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

    /// <summary>The change in value over this interval.</summary>
    [JsonPropertyName("change")]
    public decimal? Change { get; set; }

    /// <summary>The trend direction for this interval, e.g. "up". Not a confirmed closed set in source documentation.</summary>
    [JsonPropertyName("trend")]
    public string? Trend { get; set; }

    /// <summary>The number of raw data points aggregated into this interval.</summary>
    [JsonPropertyName("datapoints")]
    public int? DataPoints { get; set; }
}
