using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Profile;

/// <summary>Row order for <see cref="SteamWebApiClient.GetProfileTradesAsync"/>.</summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<ProfileTradeSort>))]
public enum ProfileTradeSort
{
    /// <summary>Most recent handovers first. The default.</summary>
    [EnumMember(Value = "newest")]
    Newest = 0,

    /// <summary>Oldest handovers first.</summary>
    [EnumMember(Value = "oldest")]
    Oldest,
}
