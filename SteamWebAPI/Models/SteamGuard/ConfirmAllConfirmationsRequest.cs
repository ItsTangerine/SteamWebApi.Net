using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.SteamGuard;

/// <summary>Request body for <see cref="SteamWebApiClient.ConfirmAllConfirmationsAsync"/>.</summary>
public sealed class ConfirmAllConfirmationsRequest
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

    /// <summary>Which category of pending confirmations to act on. Required.</summary>
    [JsonPropertyName("type")]
    public ConfirmationFilterType Type { get; set; }

    /// <summary>The action to apply to each matching confirmation. Defaults to <see cref="ConfirmationOp.Allow"/>.</summary>
    [JsonPropertyName("op")]
    public ConfirmationOp? Op { get; set; }

    /// <summary>The mobile authenticator device id.</summary>
    [JsonPropertyName("deviceid")]
    public string? DeviceId { get; set; }
}
