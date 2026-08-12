using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.TradeOffers;

/// <summary>
/// The response of <c>POST /steam/api/trade/history</c>, backed by Steam's official
/// <c>IEconService/GetTradeHistory</c>.
/// </summary>
public sealed class TradeHistoryResponse : BaseResponseDto
{
    /// <summary>The current date/time the response was generated, ISO 8601.</summary>
    [JsonPropertyName("datetime")]
    public string? DateTime { get; set; }

    /// <summary>The response type discriminator, e.g. <c>"tradehistory"</c>.</summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>The account's SteamID64.</summary>
    [JsonPropertyName("steamid")]
    public string? SteamId { get; set; }

    /// <summary>Whether the account/session is verified.</summary>
    [JsonPropertyName("verified")]
    public bool? Verified { get; set; }

    /// <summary>Pagination cursor to pass as <see cref="Models.TradeOffers.GetTradeHistoryOptions.AfterTime"/> on the next call, when more history is available.</summary>
    [JsonPropertyName("nexthistoryaftertimestamp")]
    public string? NextHistoryAfterTimestamp { get; set; }

    /// <summary>Pagination cursor to pass as <see cref="Models.TradeOffers.GetTradeHistoryOptions.AfterTrade"/> (with <see cref="NextHistoryAfterTimestamp"/>) on the next call, when more history is available.</summary>
    [JsonPropertyName("nexthistoryaftertrade")]
    public string? NextHistoryAfterTrade { get; set; }

    /// <summary>The page of trade history entries.</summary>
    [JsonPropertyName("data")]
    public IReadOnlyList<TradeHistoryEntry>? Data { get; set; }
}
