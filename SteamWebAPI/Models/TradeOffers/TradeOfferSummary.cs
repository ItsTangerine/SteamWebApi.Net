using System.Text.Json;
using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.TradeOffers;

/// <summary>
/// A trade offer summary, as returned (as an array) by <c>POST /steam/api/trade/sent</c>,
/// <c>POST /steam/api/trade/pending</c>, and <c>POST /steam/api/trade/sent/history</c>.
/// </summary>
/// <remarks>
/// steamwebapi.com does not publish a formal schema for this response; field names are reconstructed from prose
/// description only (inferred as the same conceptual shape as the single-offer object returned by
/// <c>POST /steam/api/trade/check</c>, minus the deeper <c>partner</c> detail) and should be verified against a
/// live response.
/// </remarks>
public sealed class TradeOfferSummary
{
    /// <summary>The trade offer id.</summary>
    [JsonPropertyName("tradeofferid")]
    public string? TradeOfferId { get; set; }

    /// <summary>The partner's SteamID64, auto-resolved by the server.</summary>
    [JsonPropertyName("partnersteamid")]
    public string? PartnerSteamId { get; set; }

    /// <summary>The offer's current status.</summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>The items offered by this account. Item shape is untyped; inspect each element as needed.</summary>
    [JsonPropertyName("items_to_give")]
    public IReadOnlyList<JsonElement>? ItemsToGive { get; set; }

    /// <summary>The items offered by the partner. Item shape is untyped; inspect each element as needed.</summary>
    [JsonPropertyName("items_to_receive")]
    public IReadOnlyList<JsonElement>? ItemsToReceive { get; set; }

    /// <summary>The trade offer message, when set.</summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }
}
