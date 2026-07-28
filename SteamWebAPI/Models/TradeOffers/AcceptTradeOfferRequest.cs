using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.TradeOffers;

/// <summary>Request body for <see cref="SteamWebApiClient.AcceptTradeOfferAsync"/>.</summary>
public sealed class AcceptTradeOfferRequest
{
    /// <summary>The accepting account's <c>steamLoginSecure</c> cookie value. Required.</summary>
    [JsonPropertyName("steamloginsecure")]
    public string SteamLoginSecure { get; set; } = string.Empty;

    /// <summary>The id of the trade offer to accept. Required.</summary>
    [JsonPropertyName("tradeofferid")]
    public string TradeOfferId { get; set; } = string.Empty;

    /// <summary>The partner's SteamID64. Required.</summary>
    [JsonPropertyName("partnersteamid")]
    public string PartnerSteamId { get; set; } = string.Empty;
}
