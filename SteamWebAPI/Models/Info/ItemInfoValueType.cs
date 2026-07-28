using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Info;

/// <summary>Which flat catalog of distinct values to return from <see cref="SteamWebApiClient.GetItemInfoValuesAsync"/>.</summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<ItemInfoValueType>))]
public enum ItemInfoValueType
{
    /// <summary>All known item groups (categories), e.g. "knife", "rifle".</summary>
    [EnumMember(Value = "groups")]
    Groups = 0,

    /// <summary>All known item types (weapons), e.g. "ak-47", "karambit".</summary>
    [EnumMember(Value = "types")]
    Types,

    /// <summary>All known skin names, e.g. "redline", "doppler".</summary>
    [EnumMember(Value = "items")]
    Items,
}
