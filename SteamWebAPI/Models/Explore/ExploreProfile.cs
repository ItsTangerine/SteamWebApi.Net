using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.Explore;

/// <summary>
/// A Steam profile row from steamwebapi.com's search/leaderboard index, as returned by
/// <see cref="SteamWebApiClient.GetExploreProfilesAsync"/>. This is a different shape from
/// <see cref="Profile.SteamProfile"/> (Steam-native, richer detail) despite overlapping identity/avatar fields.
/// </summary>
public sealed class ExploreProfile
{
    /// <summary>The profile's SteamID64.</summary>
    [JsonPropertyName("steamid")]
    public string SteamId { get; set; } = string.Empty;

    /// <summary>The profile's account/vanity name.</summary>
    [JsonPropertyName("accountname")]
    public string AccountName { get; set; } = string.Empty;

    /// <summary>The profile's current display/persona name.</summary>
    [JsonPropertyName("personaname")]
    public string PersonaName { get; set; } = string.Empty;

    /// <summary>An alternate display name, when steamwebapi.com has one on file.</summary>
    [JsonPropertyName("displayname")]
    public string? DisplayName { get; set; }

    /// <summary>The profile's classification, matching the <see cref="GetExploreProfilesRequest.Type"/> filter's values.</summary>
    [JsonPropertyName("profiletype")]
    public ExploreProfileType? ProfileType { get; set; }

    /// <summary>The profile's real name, when public.</summary>
    [JsonPropertyName("realname")]
    public string? RealName { get; set; }

    /// <summary>The profile's ISO 3166-1 alpha-2 country code, when public.</summary>
    [JsonPropertyName("loccountrycode")]
    public string? LocCountryCode { get; set; }

    /// <summary>The profile's summary/bio text, when public.</summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>Whether the profile is fame-flagged.</summary>
    [JsonPropertyName("fame")]
    public int Fame { get; set; }

    /// <summary>Whether the profile is VAC banned (0/1).</summary>
    [JsonPropertyName("vac")]
    public int Vac { get; set; }

    /// <summary>Whether the profile has a Limited account.</summary>
    [JsonPropertyName("islimited")]
    public bool IsLimited { get; set; }

    /// <summary>The profile's Steam level, when public.</summary>
    [JsonPropertyName("level")]
    public int? Level { get; set; }

    /// <summary>The profile's total inventory worth across all indexed games/markets.</summary>
    [JsonPropertyName("worth")]
    public double Worth { get; set; }

    /// <summary>The profile's inventory worth using Steam Market prices only.</summary>
    [JsonPropertyName("worthsteam")]
    public double WorthSteam { get; set; }

    /// <summary>The profile's total inventory item count.</summary>
    [JsonPropertyName("size")]
    public int Size { get; set; }

    /// <summary>The average worth per inventory item.</summary>
    [JsonPropertyName("peritem")]
    public double? PerItem { get; set; }

    /// <summary>Total playtime, in minutes, across all games.</summary>
    [JsonPropertyName("totalplaytime")]
    public double? TotalPlaytime { get; set; }

    /// <summary>Playtime, in minutes, in the last 2 weeks.</summary>
    [JsonPropertyName("playtimerecent")]
    public double? PlaytimeRecent { get; set; }

    /// <summary>When this Steam account was created.</summary>
    [JsonPropertyName("timecreated")]
    public DateTimeOffset? TimeCreated { get; set; }

    /// <summary>When steamwebapi.com last refreshed this profile record.</summary>
    [JsonPropertyName("updatedat")]
    public DateTimeOffset? UpdatedAt { get; set; }

    /// <summary>When steamwebapi.com last refreshed this profile's inventory.</summary>
    [JsonPropertyName("inventoryupdatedat")]
    public DateTimeOffset? InventoryUpdatedAt { get; set; }

    /// <summary>The profile's small avatar image URL.</summary>
    [JsonPropertyName("avatar")]
    public string? Avatar { get; set; }

    /// <summary>The profile's medium avatar image URL.</summary>
    [JsonPropertyName("avatarmedium")]
    public string? AvatarMedium { get; set; }

    /// <summary>The profile's full-size avatar image URL.</summary>
    [JsonPropertyName("avatarfull")]
    public string? AvatarFull { get; set; }
}
