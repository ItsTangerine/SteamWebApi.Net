using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.Items;

/// <summary>A single day's aggregated worth for the item set passed to <see cref="SteamWebApiClient.GetItemsAggregatedHistoryAsync"/>.</summary>
public sealed class AggregatedHistoryEntry
{
    /// <summary>The combined value of all requested items (including duplicates) on this day.</summary>
    [JsonPropertyName("worth")]
    public double Worth { get; set; }

    /// <summary>The number of items (including duplicates) priced on this day.</summary>
    [JsonPropertyName("size")]
    public int Size { get; set; }

    /// <summary>The date this aggregate covers.</summary>
    [JsonPropertyName("date")]
    public DateTimeOffset Date { get; set; }
}
