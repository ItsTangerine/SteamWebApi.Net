using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Common;

/// <summary>CS2 paint-wear (exterior) category, derived from the item's float value.</summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<Wear>))]
public enum Wear
{
    /// <summary>Factory New. Float range 0.00–0.07.</summary>
    [EnumMember(Value = "fn")]
    FactoryNew = 0,

    /// <summary>Minimal Wear. Float range 0.07–0.15.</summary>
    [EnumMember(Value = "mw")]
    MinimalWear,

    /// <summary>Field-Tested. Float range 0.15–0.38.</summary>
    [EnumMember(Value = "ft")]
    FieldTested,

    /// <summary>Well-Worn. Float range 0.38–0.45.</summary>
    [EnumMember(Value = "ww")]
    WellWorn,

    /// <summary>Battle-Scarred. Float range 0.45–1.00.</summary>
    [EnumMember(Value = "bs")]
    BattleScarred,
}
