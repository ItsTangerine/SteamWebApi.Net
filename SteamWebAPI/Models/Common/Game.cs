using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Common;

/// <summary>The game a pricing/item endpoint should return data for.</summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<Game>))]
public enum Game
{
    /// <summary>Counter-Strike 2 (appId 730). Full support including real-market prices and float/wear data.</summary>
    [EnumMember(Value = "cs2")]
    Cs2 = 0,

    /// <summary>Rust (appId 252490). Basic Steam market data only.</summary>
    [EnumMember(Value = "rust")]
    Rust,

    /// <summary>Dota 2 (appId 570). Basic Steam market data only.</summary>
    [EnumMember(Value = "dota")]
    Dota,

    /// <summary>Team Fortress 2 (appId 440). Basic Steam market data only.</summary>
    [EnumMember(Value = "tf2")]
    Tf2,
}
