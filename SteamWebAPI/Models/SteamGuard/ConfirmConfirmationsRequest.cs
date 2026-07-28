using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.SteamGuard;

/// <summary>
/// Request body for <see cref="SteamWebApiClient.ConfirmConfirmationsAsync"/>. Must specify either
/// <see cref="Confirmations"/> or both <see cref="ConfId"/> and <see cref="ConfKey"/>.
/// </summary>
public sealed class ConfirmConfirmationsRequest
{
    /// <summary>The account's mobile authenticator identity secret. Required.</summary>
    [JsonPropertyName("identitysecret")]
    public string IdentitySecret { get; set; } = string.Empty;

    /// <summary>The account's <c>steamLoginSecure</c> cookie value. Required.</summary>
    [JsonPropertyName("steamloginsecure")]
    public string SteamLoginSecure { get; set; } = string.Empty;

    /// <summary>The account's SteamID64. Required.</summary>
    [JsonPropertyName("steamid")]
    public string SteamId { get; set; } = string.Empty;

    /// <summary>The action to apply to the targeted confirmation(s). Required.</summary>
    [JsonPropertyName("op")]
    public ConfirmationOp Op { get; set; }

    /// <summary>One or more confirmations to act on. Alternative to <see cref="ConfId"/>/<see cref="ConfKey"/>.</summary>
    [JsonPropertyName("confirmations")]
    public IReadOnlyList<ConfirmationRef>? Confirmations { get; set; }

    /// <summary>A single confirmation id to act on. Must be paired with <see cref="ConfKey"/>. Alternative to <see cref="Confirmations"/>.</summary>
    [JsonPropertyName("confid")]
    public string? ConfId { get; set; }

    /// <summary>A single confirmation nonce to act on. Must be paired with <see cref="ConfId"/>. Alternative to <see cref="Confirmations"/>.</summary>
    [JsonPropertyName("confkey")]
    public string? ConfKey { get; set; }

    /// <summary>The mobile authenticator device id.</summary>
    [JsonPropertyName("deviceid")]
    public string? DeviceId { get; set; }
}
