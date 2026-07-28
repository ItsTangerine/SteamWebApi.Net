using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.TradeOffers;

/// <summary>Summary information about the other party to a trade offer, as returned by <c>POST /steam/api/trade/check</c>.</summary>
/// <remarks>
/// steamwebapi.com does not publish a formal schema for this response; field names are reconstructed from prose
/// description only and should be verified against a live response.
/// </remarks>
public sealed class TradePartner
{
    /// <summary>The partner's SteamID64.</summary>
    [JsonPropertyName("steamid")]
    public string? SteamId { get; set; }

    /// <summary>The partner's Steam level.</summary>
    [JsonPropertyName("level")]
    public int? Level { get; set; }

    /// <summary>The date the partner's account was created, as displayed on their profile.</summary>
    [JsonPropertyName("membersince")]
    public string? MemberSince { get; set; }

    /// <summary>Whether the partner is a Steam friend of the checking account.</summary>
    [JsonPropertyName("isfriend")]
    public bool? IsFriend { get; set; }
}
