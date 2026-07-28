namespace SteamWebAPI.Models.MarketIndex;

/// <summary>Options for <see cref="SteamWebApiClient.GetMarketIndexMultiMetricHistoryAsync"/>.</summary>
public sealed class GetMarketIndexMultiMetricHistoryRequest
{
    /// <summary>The aggregation interval. Defaults to <see cref="MarketIndexInterval.Raw"/>.</summary>
    public MarketIndexInterval? Interval { get; set; }

    /// <summary>The segmentation axis. Defaults to <see cref="MarketIndexHistorySegmentType.Global"/>.</summary>
    public MarketIndexHistorySegmentType? SegmentType { get; set; }

    /// <summary>The specific segment value, e.g. "knife". Defaults to "all".</summary>
    public string? SegmentKey { get; set; }

    /// <summary>
    /// The earliest point to include: an ISO date/datetime (<c>2026-01-01</c> or <c>2026-01-01 10:00:00</c>), a
    /// Unix timestamp, or a relative expression (<c>-24hours</c>, <c>-7days</c>, <c>-30days</c>, <c>-1year</c>).
    /// Defaults to <c>-24hours</c>.
    /// </summary>
    public string? From { get; set; }

    /// <summary>The latest point to include, in the same formats as <see cref="From"/>. Defaults to now.</summary>
    public string? To { get; set; }

    /// <summary>The maximum number of points to return per metric (1–10000). Defaults to 1000.</summary>
    public int? Limit { get; set; }
}
