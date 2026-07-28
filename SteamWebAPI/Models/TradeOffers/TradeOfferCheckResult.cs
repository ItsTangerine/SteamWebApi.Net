using System.Text.Json;
using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.TradeOffers;

/// <summary>The status/details of a trade offer, as returned by <c>POST /steam/api/trade/check</c>.</summary>
/// <remarks>
/// steamwebapi.com does not publish a formal schema for this response; field names are reconstructed from prose
/// description only and should be verified against a live response. <see cref="Status"/> is left as a plain string
/// because only a single example value was documented and the full set of possible values is not confirmed.
/// </remarks>
public sealed class TradeOfferCheckResult
{
    /// <summary>The id of the checked trade offer.</summary>
    [JsonPropertyName("tradeofferid")]
    public string? TradeOfferId { get; set; }

    /// <summary>The offer's current status, e.g. "active", "expired", "cancelled", "declined".</summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>The trade offer message, when set.</summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    /// <summary>The items offered by the recipient (this account). Item shape is untyped; inspect each element as needed.</summary>
    [JsonPropertyName("items_to_give")]
    public IReadOnlyList<JsonElement>? ItemsToGive { get; set; }

    /// <summary>The items offered by the partner. Item shape is untyped; inspect each element as needed.</summary>
    [JsonPropertyName("items_to_receive")]
    public IReadOnlyList<JsonElement>? ItemsToReceive { get; set; }

    /// <summary>The trade hold ("escrow") duration in days, if any applies.</summary>
    [JsonPropertyName("escrow_days")]
    public int? EscrowDays { get; set; }

    /// <summary>Summary information about the partner.</summary>
    [JsonPropertyName("partner")]
    public TradePartner? Partner { get; set; }
}
