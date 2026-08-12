using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.TradeOffers;

/// <summary>The outcome of <c>PUT /steam/api/trade/cancel</c> or <c>PUT /steam/api/trade/decline</c>.</summary>
/// <remarks>
/// steamwebapi.com does not publish a formal schema for this response; field names are reconstructed from prose
/// description only and should be verified against a live response.
/// </remarks>
public sealed class TradeOfferActionResult : BaseResponseDto
{
    /// <summary>Whether the action succeeded.</summary>
    [JsonPropertyName("success")]
    public bool? Success { get; set; }

    /// <summary>The id of the trade offer acted on.</summary>
    [JsonPropertyName("tradeofferid")]
    public string? TradeOfferId { get; set; }
}
