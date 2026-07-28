using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.Profile;

/// <summary>A single friend entry, as returned by <see cref="SteamWebApiClient.GetFriendListAsync"/>.</summary>
public sealed class FriendListEntry
{
    /// <summary>The friend's SteamID64.</summary>
    [JsonPropertyName("steamid")]
    public string SteamId { get; set; } = string.Empty;

    /// <summary>The friend's current display/persona name.</summary>
    [JsonPropertyName("personaname")]
    public string PersonaName { get; set; } = string.Empty;

    /// <summary>The friend's account/vanity name, falling back to their SteamID64 as a string when they have no vanity name.</summary>
    [JsonPropertyName("accountname")]
    public string AccountName { get; set; } = string.Empty;

    /// <summary>The friend's small avatar image URL.</summary>
    [JsonPropertyName("avatar")]
    public string Avatar { get; set; } = string.Empty;

    /// <summary>The friend's full-size avatar image URL.</summary>
    [JsonPropertyName("avatarfull")]
    public string AvatarFull { get; set; } = string.Empty;

    /// <summary>The friend's medium avatar image URL.</summary>
    [JsonPropertyName("avatarmedium")]
    public string AvatarMedium { get; set; } = string.Empty;

    /// <summary>The friend's Steam Community profile URL.</summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    /// <summary>Whether the friend is currently online.</summary>
    [JsonPropertyName("online")]
    public bool Online { get; set; }

    /// <summary>Whether the friend is currently in a game.</summary>
    [JsonPropertyName("in_game")]
    public bool InGame { get; set; }

    /// <summary>The name of the game the friend is currently playing, when <see cref="InGame"/> is true.</summary>
    [JsonPropertyName("game")]
    public string? Game { get; set; }
}
