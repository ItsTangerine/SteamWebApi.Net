using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.SteamGuard;

/// <summary>Request body for <see cref="SteamWebApiClient.RemoveGuardAsync"/>.</summary>
public sealed class RemoveGuardRequest
{
    /// <summary>The Steam account login name. Required.</summary>
    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    /// <summary>The Steam account password. Required.</summary>
    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;

    /// <summary>The account's current TOTP shared secret, used to log in and generate a Guard code for the removal request. Required.</summary>
    [JsonPropertyName("sharedsecret")]
    public string SharedSecret { get; set; } = string.Empty;

    /// <summary>The authenticator's revocation code, e.g. <c>"R12345"</c>. Required.</summary>
    [JsonPropertyName("revocationcode")]
    public string RevocationCode { get; set; } = string.Empty;
}
