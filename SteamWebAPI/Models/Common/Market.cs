using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Common;

/// <summary>
/// A third-party marketplace covered by steamwebapi.com's <c>markets</c> filter parameter (used to restrict which
/// markets contribute to <c>pricereal</c>/<c>prices[]</c>). This is a closed set for that parameter; the
/// <c>{market}</c> path segment used by the dedicated Market Prices endpoints accepts additional market idents
/// not in this list and is represented as a plain string there instead.
/// </summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<Market>))]
public enum Market
{
    /// <summary>Skinbaron.de</summary>
    [EnumMember(Value = "skinbaron")]
    Skinbaron = 0,

    /// <summary>Skinport.com</summary>
    [EnumMember(Value = "skinport")]
    Skinport,

    /// <summary>DMarket.com</summary>
    [EnumMember(Value = "dmarket")]
    Dmarket,

    /// <summary>Buff163.com</summary>
    [EnumMember(Value = "buff")]
    Buff,

    /// <summary>Waxpeer.com</summary>
    [EnumMember(Value = "waxpeer")]
    Waxpeer,

    /// <summary>BitSkins.com</summary>
    [EnumMember(Value = "bitskins")]
    Bitskins,

    /// <summary>CS.Money</summary>
    [EnumMember(Value = "csgotm")]
    Csgotm,

    /// <summary>HaloSkins</summary>
    [EnumMember(Value = "haloskins")]
    Haloskins,

    /// <summary>Tradeit.gg</summary>
    [EnumMember(Value = "tradeit")]
    Tradeit,

    /// <summary>Skinbid.com</summary>
    [EnumMember(Value = "skinbid")]
    Skinbid,
}
