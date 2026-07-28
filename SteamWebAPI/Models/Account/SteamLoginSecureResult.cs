using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.Account;

/// <summary>The outcome of a Steam login, as returned by <see cref="SteamWebApiClient.SteamLoginSecureAsync"/>.</summary>
public sealed class SteamLoginSecureResult
{
    /// <summary>Whether the login succeeded.</summary>
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    /// <summary>A human-readable status message.</summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    /// <summary>The session cookies/tokens obtained from Steam, for use with the Trading API.</summary>
    [JsonPropertyName("cookies")]
    public SteamLoginSecureCookies? Cookies { get; set; }
}

/// <summary>The session cookies/tokens obtained from a successful Steam login, embedded in a <see cref="SteamLoginSecureResult"/>.</summary>
public sealed class SteamLoginSecureCookies
{
    /// <summary>The <c>steamLoginSecure</c> cookie value.</summary>
    [JsonPropertyName("steamloginsecure")]
    public string? SteamLoginSecure { get; set; }

    /// <summary>When <see cref="SteamLoginSecure"/> expires.</summary>
    [JsonPropertyName("steamloginsecureexp")]
    public DateTimeOffset? SteamLoginSecureExpiresAt { get; set; }

    /// <summary>A long-lived JWT that can be used to re-authenticate without a password via <see cref="SteamLoginSecureRequest.SteamRefreshToken"/>.</summary>
    [JsonPropertyName("steamrefreshtoken")]
    public string? SteamRefreshToken { get; set; }

    /// <summary>When <see cref="SteamRefreshToken"/> expires.</summary>
    [JsonPropertyName("steamrefreshtokenexp")]
    public DateTimeOffset? SteamRefreshTokenExpiresAt { get; set; }

    /// <summary>The Steam Community session id cookie value.</summary>
    [JsonPropertyName("sessionid")]
    public string? SessionId { get; set; }

    /// <summary>The Steam browser id cookie value.</summary>
    [JsonPropertyName("browserid")]
    public string? BrowserId { get; set; }

    /// <summary>The authenticated account's 64-bit SteamID, as a string.</summary>
    [JsonPropertyName("steamid")]
    public string? SteamId { get; set; }
}
