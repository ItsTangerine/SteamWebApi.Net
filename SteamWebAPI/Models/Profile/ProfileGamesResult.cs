using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.Profile;

/// <summary>A Steam account's owned games and games-page counters, as returned by <see cref="SteamWebApiClient.GetProfileGamesAsync"/>.</summary>
public sealed class ProfileGamesResult : BaseResponseDto
{
    /// <summary>The request status, e.g. "success".</summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>The credits this call consumed (1, or 2 when the cache was bypassed).</summary>
    [JsonPropertyName("creditsused")]
    public int CreditsUsed { get; set; }

    /// <summary>The account's SteamID64.</summary>
    [JsonPropertyName("steamid")]
    public string SteamId { get; set; } = string.Empty;

    /// <summary>The account's current display/persona name.</summary>
    [JsonPropertyName("personaname")]
    public string PersonaName { get; set; } = string.Empty;

    /// <summary>The account's vanity URL slug, when it has one.</summary>
    [JsonPropertyName("profileurl")]
    public string? ProfileUrl { get; set; }

    /// <summary>
    /// Whether the account's game details are visible. <c>false</c> when they are hidden by privacy settings or the
    /// library is empty (Steam does not distinguish the two); <see cref="Games"/> is then empty.
    /// </summary>
    [JsonPropertyName("gamesvisible")]
    public bool GamesVisible { get; set; }

    /// <summary>The number of games the account owns.</summary>
    [JsonPropertyName("gamecount")]
    public int? GameCount { get; set; }

    /// <summary>The number of owned games with any recorded playtime.</summary>
    [JsonPropertyName("playedcount")]
    public int? PlayedCount { get; set; }

    /// <summary>Total all-time playtime across all owned games, in minutes.</summary>
    [JsonPropertyName("playtimeminutes")]
    public long? PlayTimeMinutes { get; set; }

    /// <summary>Total all-time playtime across all owned games, in hours.</summary>
    [JsonPropertyName("playtimehours")]
    public double? PlayTimeHours { get; set; }

    /// <summary>The number of games with every achievement unlocked.</summary>
    [JsonPropertyName("perfectgamescount")]
    public int? PerfectGamesCount { get; set; }

    /// <summary>The number of Steam reviews the account has written.</summary>
    [JsonPropertyName("reviewscount")]
    public int? ReviewsCount { get; set; }

    /// <summary>The number of games the account follows.</summary>
    [JsonPropertyName("followedcount")]
    public int? FollowedCount { get; set; }

    /// <summary>One entry per owned game, ordered by playtime (descending) then name.</summary>
    [JsonPropertyName("games")]
    public IReadOnlyList<ProfileGame> Games { get; set; } = Array.Empty<ProfileGame>();
}

/// <summary>One game owned by a Steam account, as listed in <see cref="ProfileGamesResult.Games"/>.</summary>
public sealed class ProfileGame : BaseResponseDto
{
    /// <summary>The game's Steam app id.</summary>
    [JsonPropertyName("appid")]
    public int AppId { get; set; }

    /// <summary>The game's display name.</summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>All-time playtime, in minutes.</summary>
    [JsonPropertyName("playtimeminutes")]
    public long PlayTimeMinutes { get; set; }

    /// <summary>All-time playtime, in hours.</summary>
    [JsonPropertyName("playtimehours")]
    public double PlayTimeHours { get; set; }

    /// <summary>Whether the game has any recorded playtime.</summary>
    [JsonPropertyName("played")]
    public bool Played { get; set; }

    /// <summary>Achievement progress, or <see langword="null"/> for games without achievements.</summary>
    [JsonPropertyName("achievements")]
    public ProfileGameAchievements? Achievements { get; set; }
}

/// <summary>A <see cref="ProfileGame"/>'s achievement progress.</summary>
public sealed class ProfileGameAchievements : BaseResponseDto
{
    /// <summary>The number of achievements unlocked.</summary>
    [JsonPropertyName("unlocked")]
    public int Unlocked { get; set; }

    /// <summary>The total number of achievements the game has.</summary>
    [JsonPropertyName("total")]
    public int Total { get; set; }

    /// <summary>The unlocked percentage, 0–100.</summary>
    [JsonPropertyName("percentage")]
    public double Percentage { get; set; }

    /// <summary>Whether every achievement is unlocked.</summary>
    [JsonPropertyName("perfect")]
    public bool Perfect { get; set; }
}
