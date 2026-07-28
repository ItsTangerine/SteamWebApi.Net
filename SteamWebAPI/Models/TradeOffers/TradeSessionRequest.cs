using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.TradeOffers;

/// <summary>
/// Request body carrying only a <c>steamLoginSecure</c> session, used by
/// <see cref="SteamWebApiClient.GetSentTradeOffersAsync"/>, <see cref="SteamWebApiClient.GetPendingTradeOffersAsync"/>,
/// <see cref="SteamWebApiClient.GetSentTradeOfferHistoryAsync"/>, and (as the request body, with pagination/filter
/// options carried separately in the query string) <see cref="SteamWebApiClient.GetTradeHistoryAsync"/>.
/// </summary>
public sealed class TradeSessionRequest
{
    /// <summary>The account's <c>steamLoginSecure</c> cookie value. Required.</summary>
    [JsonPropertyName("steamloginsecure")]
    public string SteamLoginSecure { get; set; } = string.Empty;
}
