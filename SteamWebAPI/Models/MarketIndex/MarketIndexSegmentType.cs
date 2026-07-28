using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.MarketIndex;

/// <summary>
/// A CS2 market index segmentation axis, used by <c>segment_type</c> on <c>GET /steam/api/market-index/cs2</c> and
/// <c>type</c> on <c>GET /steam/api/market-index/cs2/compare</c>.
/// </summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<MarketIndexSegmentType>))]
public enum MarketIndexSegmentType
{
    /// <summary>Segments by item category, e.g. "knife", "rifle", "case".</summary>
    [EnumMember(Value = "item_group")]
    ItemGroup = 0,

    /// <summary>Segments by rarity, e.g. "Covert".</summary>
    [EnumMember(Value = "rarity")]
    Rarity,

    /// <summary>Segments by exterior/wear category, e.g. "fn".</summary>
    [EnumMember(Value = "wear")]
    Wear,

    /// <summary>Segments by quality, e.g. "Normal".</summary>
    [EnumMember(Value = "quality")]
    Quality,

    /// <summary>Segments by StatTrak™ status.</summary>
    [EnumMember(Value = "stattrak")]
    StatTrak,

    /// <summary>Segments by collection.</summary>
    [EnumMember(Value = "collection")]
    Collection,
}
