using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.TradeOffers;

/// <summary>The outcome of <c>PUT /steam/api/trade/accept</c>.</summary>
/// <remarks>
/// steamwebapi.com does not publish a formal schema for this response; field names are reconstructed from prose
/// description only and should be verified against a live response.
/// </remarks>
public sealed class AcceptTradeOfferResult
{
    /// <summary>Whether the trade offer was accepted successfully.</summary>
    [JsonPropertyName("success")]
    public bool? Success { get; set; }

    /// <summary>The id of the accepted trade offer.</summary>
    [JsonPropertyName("tradeofferid")]
    public string? TradeOfferId { get; set; }
}
