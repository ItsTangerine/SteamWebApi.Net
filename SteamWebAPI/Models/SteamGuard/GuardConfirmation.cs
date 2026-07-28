using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.SteamGuard;

/// <summary>
/// A pending mobile trade/market confirmation, as returned by <c>POST /steam/api/guard/confirmations/list</c>
/// (mirrors Steam's own mobileconf/getlist shape).
/// </summary>
/// <remarks>
/// steamwebapi.com does not publish a formal schema for this response; field names are reconstructed from prose
/// description only and should be verified against a live response.
/// </remarks>
public sealed class GuardConfirmation
{
    /// <summary>The confirmation id. Passed back as <c>confirmations[].id</c> or <c>confid</c> to confirm/deny it.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>The confirmation nonce. Passed back as <c>confirmations[].key</c> or <c>confkey</c> to confirm/deny it.</summary>
    [JsonPropertyName("nonce")]
    public string? Nonce { get; set; }

    /// <summary>Steam's internal confirmation type code.</summary>
    [JsonPropertyName("type")]
    public int? Type { get; set; }

    /// <summary>The human-readable confirmation type, e.g. "Trade", "Market Listing".</summary>
    [JsonPropertyName("typename")]
    public string? TypeName { get; set; }

    /// <summary>The id of the trade offer or market listing that created this confirmation.</summary>
    [JsonPropertyName("creatorid")]
    public string? CreatorId { get; set; }

    /// <summary>A short title describing the confirmation.</summary>
    [JsonPropertyName("headline")]
    public string? Headline { get; set; }

    /// <summary>Detail lines describing what is being confirmed (e.g. items involved).</summary>
    [JsonPropertyName("summary")]
    public IReadOnlyList<string>? Summary { get; set; }

    /// <summary>A URL to an icon representing the confirmation.</summary>
    [JsonPropertyName("icon")]
    public string? Icon { get; set; }
}
