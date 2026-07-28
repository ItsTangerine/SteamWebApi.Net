using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.MarketIndex;

/// <summary>
/// A ranked comparison of segments of one type by a chosen metric, as returned by
/// <see cref="SteamWebApiClient.GetMarketIndexCompareAsync"/>.
/// </summary>
public sealed class MarketIndexCompareResult
{
    /// <summary>Whether the request succeeded.</summary>
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    /// <summary>The segmentation axis compared.</summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>The metric segments were ranked by.</summary>
    [JsonPropertyName("metric")]
    public string? Metric { get; set; }

    /// <summary>The combined metric total across all compared segments.</summary>
    [JsonPropertyName("total")]
    public decimal? Total { get; set; }

    /// <summary>The number of segments in <see cref="Segments"/>.</summary>
    [JsonPropertyName("count")]
    public int? Count { get; set; }

    /// <summary>The compared segments, sorted descending by the requested metric.</summary>
    [JsonPropertyName("segments")]
    public IReadOnlyList<SegmentComparison>? Segments { get; set; }
}

/// <summary>A single segment's ranking, embedded in a <see cref="MarketIndexCompareResult"/>.</summary>
public sealed class SegmentComparison
{
    /// <summary>The segment key, e.g. "knife".</summary>
    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;

    /// <summary>The segment's value for the requested metric.</summary>
    [JsonPropertyName("value")]
    public decimal? Value { get; set; }

    /// <summary>The number of items in this segment.</summary>
    [JsonPropertyName("itemcount")]
    public int? ItemCount { get; set; }

    /// <summary>This segment's share of <see cref="MarketIndexCompareResult.Total"/>, as a percentage.</summary>
    [JsonPropertyName("pct")]
    public decimal? Pct { get; set; }
}
