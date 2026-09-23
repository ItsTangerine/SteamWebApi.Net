using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.Items;

/// <summary>One item in <see cref="GetItemsAggregatedHistoryRequest.Entries"/>.</summary>
public sealed class AggregatedHistoryItem
{
    /// <summary>The item's Steam Market hash name. Required.</summary>
    [JsonPropertyName("market_hash_name")]
    public string MarketHashName { get; set; } = string.Empty;

    /// <summary>
    /// The Doppler-style phase, e.g. "Phase 2". When set, third-party prices must match this exact phase; Steam
    /// remains a generic candidate.
    /// </summary>
    [JsonPropertyName("phase")]
    public string? Phase { get; set; }

    /// <summary>The paint index of the phase, e.g. 419.</summary>
    [JsonPropertyName("paint_index")]
    public int? PaintIndex { get; set; }

    /// <summary>How many copies to count. Defaults to 1.</summary>
    [JsonPropertyName("count")]
    public int? Count { get; set; }
}
