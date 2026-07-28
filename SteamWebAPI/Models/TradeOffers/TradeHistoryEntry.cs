using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.TradeOffers;

/// <summary>A single completed trade, as returned within <see cref="TradeHistoryResponse.Data"/>.</summary>
public sealed class TradeHistoryEntry
{
    /// <summary>The Steam trade id, e.g. <c>"815844385241622220"</c>.</summary>
    [JsonPropertyName("tradeid")]
    public string? TradeId { get; set; }

    /// <summary>The trade date/time, ISO 8601 UTC, e.g. <c>"2025-12-16T05:09:40+00:00"</c>.</summary>
    [JsonPropertyName("date")]
    public DateTimeOffset? Date { get; set; }

    /// <summary>The trade date/time as a Unix timestamp.</summary>
    [JsonPropertyName("timestamp")]
    public long? Timestamp { get; set; }

    /// <summary>The trade partner's SteamID64 (Steam's GetTradeHistory does not expose a username, so this mirrors <see cref="ParticipantSteamId"/>).</summary>
    [JsonPropertyName("participantusername")]
    public string? ParticipantUserName { get; set; }

    /// <summary>The trade partner's Steam profile URL.</summary>
    [JsonPropertyName("participanturl")]
    public string? ParticipantUrl { get; set; }

    /// <summary>The trade partner's SteamID64.</summary>
    [JsonPropertyName("participantsteamid")]
    public string? ParticipantSteamId { get; set; }

    /// <summary>Whether the trade completed normally or was reversed.</summary>
    [JsonPropertyName("status")]
    public TradeHistoryStatus? Status { get; set; }

    /// <summary>Whether the trade is still within its 7-day trade protection period.</summary>
    [JsonPropertyName("tradeprotected")]
    public bool? TradeProtected { get; set; }

    /// <summary>The date trade protection ends, as <c>Y-m-d</c>, when applicable.</summary>
    [JsonPropertyName("tradeprotecteduntil")]
    public string? TradeProtectedUntil { get; set; }

    /// <summary>The Unix timestamp trade protection ends, from Steam's <c>time_settlement</c>, when applicable.</summary>
    [JsonPropertyName("tradeprotecteduntiltimestamp")]
    public long? TradeProtectedUntilTimestamp { get; set; }

    /// <summary>A human-readable summary of the trade, e.g. <c>You traded with "76561198769457065".</c>.</summary>
    [JsonPropertyName("tradeinfo")]
    public string? TradeInfo { get; set; }

    /// <summary>Reversal details, present only when the trade was reversed.</summary>
    [JsonPropertyName("tradereturned")]
    public TradeReturned? TradeReturned { get; set; }

    /// <summary>The items exchanged in this trade.</summary>
    [JsonPropertyName("items")]
    public TradeItems? Items { get; set; }
}
