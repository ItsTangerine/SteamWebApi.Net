using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.TradeOffers;

/// <summary>The outcome of <c>POST /steam/api/trade/create</c>.</summary>
/// <remarks>
/// steamwebapi.com does not publish a formal schema for this response; field names are reconstructed from prose
/// description only and should be verified against a live response.
/// </remarks>
public sealed class CreateTradeOfferResult
{
    /// <summary>Whether the trade offer was created successfully.</summary>
    [JsonPropertyName("success")]
    public bool? Success { get; set; }

    /// <summary>The id of the newly created trade offer.</summary>
    [JsonPropertyName("tradeofferid")]
    public string? TradeOfferId { get; set; }

    /// <summary>Whether the partner (or sender) must approve the offer via a mobile authenticator confirmation before it proceeds.</summary>
    [JsonPropertyName("needs_mobile_confirmation")]
    public bool? NeedsMobileConfirmation { get; set; }

    /// <summary>Whether the offer must be approved via an emailed confirmation link before it proceeds.</summary>
    [JsonPropertyName("needs_email_confirmation")]
    public bool? NeedsEmailConfirmation { get; set; }
}
