using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.SteamGuard;

/// <summary>A single confirmation identifier/nonce pair, used to batch-confirm via <see cref="ConfirmConfirmationsRequest.Confirmations"/>.</summary>
public sealed class ConfirmationRef
{
    /// <summary>The confirmation id, from <see cref="GuardConfirmation.Id"/>.</summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>The confirmation nonce, from <see cref="GuardConfirmation.Nonce"/>.</summary>
    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;
}
