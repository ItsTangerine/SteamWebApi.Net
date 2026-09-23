using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Common;

/// <summary>
/// How far back a player-count history reaches, for <see cref="SteamWebApiClient.GetPlatformPlayersAsync"/> and
/// <see cref="SteamWebApiClient.GetAppPlayersAsync"/>. The resolution of each value depends on the endpoint.
/// </summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<PlayerHistoryRange>))]
public enum PlayerHistoryRange
{
    /// <summary>The last 24 hours.</summary>
    [EnumMember(Value = "24h")]
    Hours24 = 0,

    /// <summary>The last 7 days.</summary>
    [EnumMember(Value = "7d")]
    Days7,

    /// <summary>The last 30 days.</summary>
    [EnumMember(Value = "30d")]
    Days30,

    /// <summary>The last 90 days.</summary>
    [EnumMember(Value = "90d")]
    Days90,

    /// <summary>The last year.</summary>
    [EnumMember(Value = "1y")]
    Year1,

    /// <summary>All recorded history.</summary>
    [EnumMember(Value = "all")]
    All,
}
