using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.Account;

/// <summary>
/// Request body for <see cref="SteamWebApiClient.SteamLoginSecureAsync"/>. Exactly one authentication mode must be
/// supplied: <see cref="Username"/> + <see cref="Password"/> (optionally with <see cref="Code"/>), or
/// <see cref="SteamRefreshToken"/> alone.
/// </summary>
public sealed class SteamLoginSecureRequest
{
    /// <summary>The Steam username. Omit when using <see cref="SteamRefreshToken"/>.</summary>
    [JsonPropertyName("username")]
    public string? Username { get; set; }

    /// <summary>The Steam password. Omit when using <see cref="SteamRefreshToken"/>.</summary>
    [JsonPropertyName("password")]
    public string? Password { get; set; }

    /// <summary>
    /// A Steam Guard code derived from the mobile authenticator's shared secret — not the literal code shown on
    /// screen in the Steam mobile app. Only meaningful alongside <see cref="Username"/>/<see cref="Password"/>.
    /// </summary>
    [JsonPropertyName("code")]
    public string? Code { get; set; }

    /// <summary>A long-lived JWT from a prior login, used as an alternative to <see cref="Username"/> + <see cref="Password"/>.</summary>
    [JsonPropertyName("steamrefreshtoken")]
    public string? SteamRefreshToken { get; set; }
}
