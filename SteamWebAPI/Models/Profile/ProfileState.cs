using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Profile;

/// <summary>How much profile detail to return from <see cref="SteamWebApiClient.GetProfileAsync"/>/<see cref="SteamWebApiClient.GetProfileBatchAsync"/>.</summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<ProfileState>))]
public enum ProfileState
{
    /// <summary>Identity, avatar, and presence fields only. The default; fastest option.</summary>
    [EnumMember(Value = "minimal")]
    Minimal = 0,

    /// <summary>Full detail, including most-played games and other enrichment fields.</summary>
    [EnumMember(Value = "full")]
    Full,
}
