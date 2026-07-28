using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Explore;

/// <summary>The classification of a notable Steam profile in steamwebapi.com's explore index.</summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<ExploreProfileType>))]
public enum ExploreProfileType
{
    /// <summary>A professional esports player.</summary>
    [EnumMember(Value = "pro")]
    Pro = 0,

    /// <summary>A content streamer.</summary>
    [EnumMember(Value = "streamer")]
    Streamer,

    /// <summary>A VIP-flagged profile.</summary>
    [EnumMember(Value = "vip")]
    Vip,

    /// <summary>A notable personality.</summary>
    [EnumMember(Value = "personality")]
    Personality,

    /// <summary>A content creator.</summary>
    [EnumMember(Value = "content_creator")]
    ContentCreator,

    /// <summary>A caster/commentator.</summary>
    [EnumMember(Value = "caster")]
    Caster,
}
