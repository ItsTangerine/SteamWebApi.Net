using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.TradeOffers;

/// <summary>
/// Request body for <see cref="SteamWebApiClient.CancelTradeOfferAsync"/> and
/// <see cref="SteamWebApiClient.DeclineTradeOfferAsync"/>.
/// </summary>
public sealed class TradeOfferActionRequest
{
    /// <summary>The account's <c>steamLoginSecure</c> cookie value. Required.</summary>
    [JsonPropertyName("steamloginsecure")]
    public string SteamLoginSecure { get; set; } = string.Empty;

    /// <summary>The id of the trade offer to act on. Required.</summary>
    [JsonPropertyName("tradeofferid")]
    public string TradeOfferId { get; set; } = string.Empty;
}
