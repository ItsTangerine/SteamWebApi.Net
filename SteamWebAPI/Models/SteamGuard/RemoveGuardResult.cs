using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.SteamGuard;

/// <summary>The outcome of <c>POST /steam/api/guard/remove</c>.</summary>
/// <remarks>
/// steamwebapi.com does not publish a formal schema for this response; field names are reconstructed from prose
/// description only and should be verified against a live response.
/// </remarks>
public sealed class RemoveGuardResult
{
    /// <summary>Whether the authenticator was successfully removed.</summary>
    [JsonPropertyName("success")]
    public bool? Success { get; set; }

    /// <summary>A human-readable status/error message.</summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }
}
