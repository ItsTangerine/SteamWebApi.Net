using System.Text.Json;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Profile;

/// <summary>
/// A Steam-native user profile, as returned by <see cref="SteamWebApiClient.GetProfileAsync"/> and
/// <see cref="SteamWebApiClient.GetProfileBatchAsync"/>. This is a different shape from <see cref="Explore.ExploreProfile"/>
/// (search/leaderboard-oriented) and <see cref="FriendListEntry"/> (minimal presence-oriented).
/// </summary>
public sealed class SteamProfile : BaseResponseDto
{
    /// <summary>The profile's SteamID64.</summary>
    [JsonPropertyName("steamid")]
    public string SteamId { get; set; } = string.Empty;

    /// <summary>The profile's current display/persona name.</summary>
    [JsonPropertyName("personaname")]
    public string PersonaName { get; set; } = string.Empty;

    /// <summary>The profile's account/vanity name.</summary>
    [JsonPropertyName("accountname")]
    public string AccountName { get; set; } = string.Empty;

    /// <summary>The profile's vanity URL slug, when it has one.</summary>
    [JsonPropertyName("profileurl")]
    public string? ProfileUrl { get; set; }

    /// <summary>The profile's full Steam Community URL.</summary>
    [JsonPropertyName("profilesteamurl")]
    public string ProfileSteamUrl { get; set; } = string.Empty;

    /// <summary>The hash identifying the profile's current avatar image.</summary>
    [JsonPropertyName("avatarhash")]
    public string AvatarHash { get; set; } = string.Empty;

    /// <summary>The profile's small avatar image URL.</summary>
    [JsonPropertyName("avatar")]
    public string Avatar { get; set; } = string.Empty;

    /// <summary>The profile's medium avatar image URL.</summary>
    [JsonPropertyName("avatarmedium")]
    public string AvatarMedium { get; set; } = string.Empty;

    /// <summary>The profile's full-size avatar image URL.</summary>
    [JsonPropertyName("avatarfull")]
    public string AvatarFull { get; set; } = string.Empty;

    /// <summary>The profile's real name, when public.</summary>
    [JsonPropertyName("realname")]
    public string? RealName { get; set; }

    /// <summary>A human-readable description of <see cref="CommunityVisibilityState"/>, e.g. "public".</summary>
    [JsonPropertyName("communityvisibilitymessage")]
    public string CommunityVisibilityMessage { get; set; } = string.Empty;

    /// <summary>The Steam community visibility code (1 = private, 3 = public).</summary>
    [JsonPropertyName("communityvisibilitystate")]
    public int CommunityVisibilityState { get; set; }

    /// <summary>The Steam profile setup state code.</summary>
    [JsonPropertyName("profilestate")]
    public int ProfileState { get; set; }

    /// <summary>The profile's current online presence, e.g. "in-game", "offline", "online".</summary>
    [JsonPropertyName("onlinestate")]
    public string OnlineState { get; set; } = string.Empty;

    /// <summary>
    /// The current in-game details (game name/link), when the profile is in-game. steamwebapi.com returns this as
    /// an object when populated but as an empty array when not (observed in minimal/batch responses), so it is
    /// exposed as raw JSON; deserialize to an object shape (<c>gamename</c>/<c>gamelink</c>) when
    /// <see cref="JsonElement.ValueKind"/> is <see cref="JsonValueKind.Object"/>.
    /// </summary>
    [JsonPropertyName("ingameinfo")]
    public JsonElement? IngameInfo { get; set; }

    /// <summary>When this Steam account was created.</summary>
    [JsonPropertyName("timecreated")]
    [JsonConverter(typeof(UnixSecondsConverter))]
    public DateTimeOffset? TimeCreated { get; set; }

    /// <summary>When this Steam account was created, as an ISO 8601 timestamp.</summary>
    [JsonPropertyName("timecreatedat")]
    public DateTimeOffset? TimeCreatedAt { get; set; }

    /// <summary>The profile's location text, when public.</summary>
    [JsonPropertyName("location")]
    public string? Location { get; set; }

    /// <summary>The profile's ISO 3166-1 alpha-2 country code, when public.</summary>
    [JsonPropertyName("loccountrycode")]
    public string? LocCountryCode { get; set; }

    /// <summary>The profile's summary/bio text, when public.</summary>
    [JsonPropertyName("summary")]
    public string? Summary { get; set; }

    /// <summary>Whether the profile is VAC banned (0/1).</summary>
    [JsonPropertyName("vac")]
    public int Vac { get; set; }

    /// <summary>Whether the profile has a Limited account (0/1).</summary>
    [JsonPropertyName("islimited")]
    public int IsLimited { get; set; }

    /// <summary>Total all-time playtime, in minutes, across the profile's most-played games. Populated only for <see cref="ProfileState.Full"/>.</summary>
    [JsonPropertyName("mostplayedgamestotalplaytime")]
    public double? MostPlayedGamesTotalPlayTime { get; set; }

    /// <summary>Total playtime in the last 2 weeks, in minutes, across the profile's most-played games.</summary>
    [JsonPropertyName("mostplayedgames2weeksplaytime")]
    public double? MostPlayedGames2WeeksPlayTime { get; set; }

    /// <summary>The app ids of the profile's most-played games.</summary>
    [JsonPropertyName("mostplayedgamesappids")]
    public IReadOnlyList<int>? MostPlayedGamesAppIds { get; set; }

    /// <summary>The profile's most-played games, with names and icons.</summary>
    [JsonPropertyName("mostplayedgames")]
    public IReadOnlyList<MostPlayedGame>? MostPlayedGames { get; set; }

    /// <summary>The profile's most-played games, with playtime figures only.</summary>
    [JsonPropertyName("mostplayedgamestimes")]
    public IReadOnlyList<MostPlayedGameTime>? MostPlayedGamesTimes { get; set; }

    /// <summary>The profile's Steam group memberships, present only when <c>with_groups=1</c> was requested.</summary>
    [JsonPropertyName("groups")]
    public IReadOnlyList<SteamGroup>? Groups { get; set; }

    /// <summary>
    /// The Steam persona state code (0 = offline, 1 = online, etc.). Observed as an extra field on
    /// minimal/batch responses; not present on the enriched single-profile shape.
    /// </summary>
    [JsonPropertyName("personastate")]
    public int? PersonaState { get; set; }
    
    /// <summary>Whether the profile has a Steam Community trade ban (0/1). Present for full profile responses.</summary>
    [JsonPropertyName("tradeban")]
    public int? TradeBan { get; set; }

    /// <summary>A human-readable description of the profile's current presence. Present for full profile responses.</summary>
    [JsonPropertyName("statemessage")]
    public string? StateMessage { get; set; }

    /// <summary>The availability state of the profile's friends list. Present for full profile responses.</summary>
    [JsonPropertyName("friendsstate")]
    public int? FriendsState { get; set; }

    /// <summary>The number of friends associated with the profile. Present for full profile responses.</summary>
    [JsonPropertyName("friendscount")]
    public int? FriendsCount { get; set; }

    /// <summary>The number of games owned by the profile. Present for full profile responses.</summary>
    [JsonPropertyName("gamescount")]
    public int? GamesCount { get; set; }

    /// <summary>The number of Steam groups the profile belongs to. Present for full profile responses.</summary>
    [JsonPropertyName("groupscount")]
    public int? GroupsCount { get; set; }

    /// <summary>The number of Steam badges earned by the profile. Present for full profile responses.</summary>
    [JsonPropertyName("badgescount")]
    public int? BadgesCount { get; set; }

    /// <summary>The number of game bans recorded for the profile. Present for full profile responses.</summary>
    [JsonPropertyName("gameban")]
    public int? GameBan { get; set; }

    /// <summary>
    /// The number of days since the profile's most recent game ban. Present for full profile responses
    /// and potentially <see langword="null"/> even when requested.
    /// </summary>
    [JsonPropertyName("lastbandays")]
    public int? LastBanDays { get; set; }

    /// <summary>The profile's Steam level. Present for full profile responses.</summary>
    [JsonPropertyName("level")]
    public int? Level { get; set; }
}

/// <summary>One of a <see cref="SteamProfile"/>'s most-played games, with name/icon metadata.</summary>
public sealed class MostPlayedGame : BaseResponseDto
{
    /// <summary>The game's display name.</summary>
    [JsonPropertyName("gamename")]
    public string GameName { get; set; } = string.Empty;

    /// <summary>The Steam store/community link for the game.</summary>
    [JsonPropertyName("gamelink")]
    public string GameLink { get; set; } = string.Empty;

    /// <summary>The game's Steam app id.</summary>
    [JsonPropertyName("appid")]
    public int AppId { get; set; }

    /// <summary>The game's icon image URL.</summary>
    [JsonPropertyName("gameicon")]
    public string GameIcon { get; set; } = string.Empty;

    /// <summary>The game's logo image URL.</summary>
    [JsonPropertyName("gamelogo")]
    public string GameLogo { get; set; } = string.Empty;

    /// <summary>The game's small logo image URL.</summary>
    [JsonPropertyName("gamelogosmall")]
    public string GameLogoSmall { get; set; } = string.Empty;

    /// <summary>Playtime in the last 2 weeks, in minutes.</summary>
    [JsonPropertyName("playtimelast2weeks")]
    public double PlayTimeLast2Weeks { get; set; }

    /// <summary>Total all-time playtime, in hours.</summary>
    [JsonPropertyName("hoursonrecord")]
    public int HoursOnRecord { get; set; }

    /// <summary>The game's Steam community stats page identifier.</summary>
    [JsonPropertyName("statsname")]
    public string StatsName { get; set; } = string.Empty;
}

/// <summary>Playtime figures for one of a <see cref="SteamProfile"/>'s most-played games, without name/icon metadata.</summary>
public sealed class MostPlayedGameTime : BaseResponseDto
{
    /// <summary>The game's Steam app id.</summary>
    [JsonPropertyName("appid")]
    public int AppId { get; set; }

    /// <summary>Playtime in the last 2 weeks, in minutes.</summary>
    [JsonPropertyName("playtimelast2weeks")]
    public double PlayTimeLast2Weeks { get; set; }

    /// <summary>Total all-time playtime, in hours.</summary>
    [JsonPropertyName("hoursonrecord")]
    public int HoursOnRecord { get; set; }
}

/// <summary>A Steam group a <see cref="SteamProfile"/> belongs to.</summary>
public sealed class SteamGroup : BaseResponseDto
{
    /// <summary>The group's Steam id.</summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>The group's display name.</summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>The group's Steam Community URL.</summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    /// <summary>Whether this is the profile's primary (featured) group.</summary>
    [JsonPropertyName("primary")]
    public bool Primary { get; set; }

    /// <summary>The group's small avatar image URL.</summary>
    [JsonPropertyName("avatar")]
    public string Avatar { get; set; } = string.Empty;

    /// <summary>The group's medium avatar image URL.</summary>
    [JsonPropertyName("avatarmedium")]
    public string AvatarMedium { get; set; } = string.Empty;

    /// <summary>The group's full-size avatar image URL.</summary>
    [JsonPropertyName("avatarfull")]
    public string? AvatarFull { get; set; }

    /// <summary>The group's member count.</summary>
    [JsonPropertyName("membercount")]
    public int MemberCount { get; set; }
}
