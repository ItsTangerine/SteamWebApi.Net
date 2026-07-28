using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.TradeOffers;

/// <summary>Reversal details for a trade, present on <see cref="TradeHistoryEntry.TradeReturned"/> only when the trade was reversed.</summary>
public sealed class TradeReturned
{
    /// <summary>Whether the trade was reversed.</summary>
    [JsonPropertyName("tradeReversed")]
    public bool? TradeReversed { get; set; }

    /// <summary>When the reversal occurred, as UTC <c>Y-m-d H:i:s</c>.</summary>
    [JsonPropertyName("reversalAt")]
    public string? ReversalAt { get; set; }

    /// <summary>The reason given for the reversal, when available.</summary>
    [JsonPropertyName("reversalReason")]
    public string? ReversalReason { get; set; }

    /// <summary>The reversal status, e.g. "reversed".</summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }
}
