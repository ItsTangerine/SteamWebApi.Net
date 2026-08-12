using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.SteamGuard;

/// <summary>The outcome of <c>POST /steam/api/guard/confirmations/confirm-all</c>.</summary>
/// <remarks>
/// steamwebapi.com does not publish a formal schema for this response; field names are reconstructed from prose
/// description only and should be verified against a live response.
/// </remarks>
public sealed class ConfirmAllConfirmationsResult : BaseResponseDto
{
    /// <summary>The number of confirmations acted on.</summary>
    [JsonPropertyName("count")]
    public int? Count { get; set; }

    /// <summary>The per-confirmation outcomes.</summary>
    [JsonPropertyName("confirmations")]
    public IReadOnlyList<ConfirmationActionResult>? Confirmations { get; set; }
}
