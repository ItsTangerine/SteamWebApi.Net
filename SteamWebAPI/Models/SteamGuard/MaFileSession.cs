using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.SteamGuard;

/// <summary>The <c>Session</c> object nested inside a Steam Desktop Authenticator maFile.</summary>
public sealed class MaFileSession
{
    /// <summary>The web session id.</summary>
    [JsonPropertyName("SessionID")]
    public string? SessionId { get; set; }

    /// <summary>The legacy <c>steamLogin</c> cookie value.</summary>
    [JsonPropertyName("SteamLogin")]
    public string? SteamLogin { get; set; }

    /// <summary>The <c>steamLoginSecure</c> cookie value.</summary>
    [JsonPropertyName("SteamLoginSecure")]
    public string? SteamLoginSecure { get; set; }

    /// <summary>The web cookie value.</summary>
    [JsonPropertyName("WebCookie")]
    public string? WebCookie { get; set; }

    /// <summary>The OAuth token for this session.</summary>
    [JsonPropertyName("OAuthToken")]
    public string? OAuthToken { get; set; }

    /// <summary>The account's SteamID64.</summary>
    [JsonPropertyName("SteamID")]
    public string? SteamId { get; set; }
}
