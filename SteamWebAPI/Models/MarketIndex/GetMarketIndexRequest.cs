namespace SteamWebAPI.Models.MarketIndex;

/// <summary>Options for <see cref="SteamWebApiClient.GetMarketIndexAsync"/>.</summary>
public sealed class GetMarketIndexRequest
{
    /// <summary>
    /// The segmentation axis to filter by. When omitted, the global overview across all segments is returned.
    /// Requires <see cref="SegmentKey"/> to also be set.
    /// </summary>
    public MarketIndexSegmentType? SegmentType { get; set; }

    /// <summary>
    /// The specific segment value to return stats for, e.g. "knife", "Covert", "fn". Required when
    /// <see cref="SegmentType"/> is set. Call without any options to discover valid keys via the response's
    /// available-segments data.
    /// </summary>
    public string? SegmentKey { get; set; }
}
