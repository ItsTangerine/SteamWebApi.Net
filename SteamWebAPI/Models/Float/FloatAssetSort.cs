using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Float;

/// <summary>Sort order for <see cref="SteamWebApiClient.SearchFloatAssetsAsync"/> results.</summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<FloatAssetSort>))]
public enum FloatAssetSort
{
    /// <summary>Most recently seen/created first. The default.</summary>
    [EnumMember(Value = "newest")]
    Newest = 0,

    /// <summary>Least recently seen/created first.</summary>
    [EnumMember(Value = "oldest")]
    Oldest,

    /// <summary>Lowest float value first.</summary>
    [EnumMember(Value = "lowest_float")]
    LowestFloat,

    /// <summary>Highest float value first.</summary>
    [EnumMember(Value = "highest_float")]
    HighestFloat,
}
