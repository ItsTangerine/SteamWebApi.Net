using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.SteamGuard;

/// <summary>The outcome of acting on a single confirmation, as returned (always inside an array) by the confirm/confirm-all endpoints.</summary>
/// <remarks>
/// steamwebapi.com does not publish a formal schema for this response; field names are reconstructed from prose
/// description only and should be verified against a live response.
/// </remarks>
public sealed class ConfirmationActionResult
{
    /// <summary>The id of the confirmation acted on.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>Whether the action succeeded.</summary>
    [JsonPropertyName("success")]
    public bool? Success { get; set; }

    /// <summary>An error detail, present only when <see cref="Success"/> is <c>false</c>.</summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }
}
