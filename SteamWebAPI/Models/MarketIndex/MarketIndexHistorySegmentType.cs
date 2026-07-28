using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.MarketIndex;

/// <summary>
/// A CS2 market index segmentation axis for the history endpoints, used by <c>segment_type</c> on
/// <c>GET /steam/api/market-index/cs2/history</c>. Unlike <see cref="MarketIndexSegmentType"/>, this includes
/// <see cref="Global"/> as an explicit, selectable (and default) value.
/// </summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<MarketIndexHistorySegmentType>))]
public enum MarketIndexHistorySegmentType
{
    /// <summary>The whole market, not segmented. The default.</summary>
    [EnumMember(Value = "global")]
    Global = 0,

    /// <summary>Segments by item category, e.g. "knife", "rifle", "case".</summary>
    [EnumMember(Value = "item_group")]
    ItemGroup,

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
