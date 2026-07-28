using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.TradeOffers;

/// <summary>The items exchanged in a single <see cref="TradeHistoryEntry"/>.</summary>
public sealed class TradeItems
{
    /// <summary>The items received from the partner.</summary>
    [JsonPropertyName("received")]
    public IReadOnlyList<TradeHistoryItem>? Received { get; set; }

    /// <summary>The items sent to the partner.</summary>
    [JsonPropertyName("sent")]
    public IReadOnlyList<TradeHistoryItem>? Sent { get; set; }
}
