using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.Profile;

/// <summary>The escrow/trade-hold status of a Steam trade URL, as returned by <see cref="SteamWebApiClient.GetTradeEligibilityAsync"/>.</summary>
public sealed class TradeEligibilityResult : BaseResponseDto
{
    /// <summary>Whether the trade URL is well-formed and currently valid.</summary>
    [JsonPropertyName("tradeurlvalid")]
    public bool TradeUrlValid { get; set; }

    /// <summary>The number of days a trade with this user would be held in escrow.</summary>
    [JsonPropertyName("escrowdays")]
    public int EscrowDays { get; set; }

    /// <summary>Whether a trade with this user would be held in escrow at all.</summary>
    [JsonPropertyName("isescrow")]
    public bool IsEscrow { get; set; }

    /// <summary>
    /// The trade status, e.g. "instant". Only "instant" is exemplified by steamwebapi.com; other values (e.g. for
    /// delayed/held trades) are presumed to exist but are undocumented, so this is free text rather than a closed
    /// enum.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;
}
