using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.TradeOffers;

/// <summary>Request body for <see cref="SteamWebApiClient.CreateTradeOfferAsync"/>.</summary>
public sealed class CreateTradeOfferRequest
{
    /// <summary>The sender's <c>steamLoginSecure</c> cookie value, in the form <c>"&lt;steamid&gt;||&lt;jwt&gt;"</c>. Required.</summary>
    [JsonPropertyName("steamloginsecure")]
    public string SteamLoginSecure { get; set; } = string.Empty;

    /// <summary>The partner's full Steam trade offer URL. Required.</summary>
    [JsonPropertyName("tradelink")]
    public string TradeLink { get; set; } = string.Empty;

    /// <summary>The partner's SteamID64. Required.</summary>
    [JsonPropertyName("partnersteamid")]
    public string PartnerSteamId { get; set; } = string.Empty;

    /// <summary>The trade offer message shown to the partner. Required.</summary>
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;

    /// <summary>The asset ids of the items being requested from the partner.</summary>
    [JsonPropertyName("partneritemassetids")]
    public IReadOnlyList<string>? PartnerItemAssetIds { get; set; }

    /// <summary>The asset ids of the items being offered by the sender.</summary>
    [JsonPropertyName("myitemassetids")]
    public IReadOnlyList<string>? MyItemAssetIds { get; set; }

    /// <summary>The game context for the offered/requested items. Defaults to <c>"cs2"</c>. Free text — other Steam game identifiers are likely valid.</summary>
    [JsonPropertyName("game")]
    public string? Game { get; set; }
}
