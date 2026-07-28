using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.TradeOffers;

/// <summary>Request body for <see cref="SteamWebApiClient.CheckTradeOfferAsync"/>.</summary>
public sealed class CheckTradeOfferRequest
{
    /// <summary>
    /// The <c>steamLoginSecure</c> cookie value of the offer's <em>recipient</em>. Required. Using the sender's
    /// session fails — this endpoint is recipient-only.
    /// </summary>
    [JsonPropertyName("steamloginsecure")]
    public string SteamLoginSecure { get; set; } = string.Empty;

    /// <summary>The id of the trade offer to check. Required.</summary>
    [JsonPropertyName("tradeofferid")]
    public string TradeOfferId { get; set; } = string.Empty;
}
