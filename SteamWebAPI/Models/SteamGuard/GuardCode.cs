using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.SteamGuard;

/// <summary>
/// A freshly generated Steam Guard TOTP login code, as returned by <c>POST /steam/api/guard/code</c>.
/// </summary>
/// <remarks>
/// steamwebapi.com does not publish a formal schema for this response; field names are reconstructed from prose
/// description only and should be verified against a live response.
/// </remarks>
public sealed class GuardCode
{
    /// <summary>The current 5-character Steam Guard login code.</summary>
    [JsonPropertyName("code")]
    public string? Code { get; set; }

    /// <summary>The Unix timestamp at which <see cref="Code"/> expires, when provided.</summary>
    [JsonPropertyName("expires")]
    public long? Expires { get; set; }

    /// <summary>The server's current Unix timestamp at the time the code was generated, when provided.</summary>
    [JsonPropertyName("servertime")]
    public long? ServerTime { get; set; }
}
