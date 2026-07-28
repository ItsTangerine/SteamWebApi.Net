using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.SteamGuard;

/// <summary>Request body for <see cref="SteamWebApiClient.GenerateGuardCodeAsync"/>.</summary>
public sealed class GenerateGuardCodeRequest
{
    /// <summary>The account's base64/hex TOTP shared secret. Required. Never persisted server-side.</summary>
    [JsonPropertyName("sharedsecret")]
    public string SharedSecret { get; set; } = string.Empty;

    /// <summary>The account's SteamID64, used only to derive <see cref="DeviceId"/> when it is omitted.</summary>
    [JsonPropertyName("steamid")]
    public string? SteamId { get; set; }

    /// <summary>The mobile authenticator device id.</summary>
    [JsonPropertyName("deviceid")]
    public string? DeviceId { get; set; }
}
