using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Inventory;

/// <summary>The localization language for inventory item names/descriptions.</summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<InventoryLanguage>))]
public enum InventoryLanguage
{
    /// <summary>English. The default.</summary>
    [EnumMember(Value = "english")]
    English = 0,

    /// <summary>Arabic.</summary>
    [EnumMember(Value = "arabic")]
    Arabic,

    /// <summary>Bulgarian.</summary>
    [EnumMember(Value = "bulgarian")]
    Bulgarian,

    /// <summary>Simplified Chinese.</summary>
    [EnumMember(Value = "schinese")]
    SChinese,

    /// <summary>Traditional Chinese.</summary>
    [EnumMember(Value = "tchinese")]
    TChinese,

    /// <summary>Czech.</summary>
    [EnumMember(Value = "czech")]
    Czech,

    /// <summary>Danish.</summary>
    [EnumMember(Value = "danish")]
    Danish,

    /// <summary>Dutch.</summary>
    [EnumMember(Value = "dutch")]
    Dutch,

    /// <summary>Finnish.</summary>
    [EnumMember(Value = "finnish")]
    Finnish,

    /// <summary>French.</summary>
    [EnumMember(Value = "french")]
    French,

    /// <summary>German.</summary>
    [EnumMember(Value = "german")]
    German,

    /// <summary>Greek.</summary>
    [EnumMember(Value = "greek")]
    Greek,

    /// <summary>Hungarian.</summary>
    [EnumMember(Value = "hungarian")]
    Hungarian,

    /// <summary>Indonesian.</summary>
    [EnumMember(Value = "indonesian")]
    Indonesian,

    /// <summary>Italian.</summary>
    [EnumMember(Value = "italian")]
    Italian,

    /// <summary>Japanese.</summary>
    [EnumMember(Value = "japanese")]
    Japanese,

    /// <summary>Korean.</summary>
    [EnumMember(Value = "koreana")]
    Koreana,

    /// <summary>Norwegian.</summary>
    [EnumMember(Value = "norwegian")]
    Norwegian,

    /// <summary>Polish.</summary>
    [EnumMember(Value = "polish")]
    Polish,

    /// <summary>Portuguese.</summary>
    [EnumMember(Value = "portuguese")]
    Portuguese,

    /// <summary>Brazilian Portuguese.</summary>
    [EnumMember(Value = "brazilian")]
    Brazilian,

    /// <summary>Romanian.</summary>
    [EnumMember(Value = "romanian")]
    Romanian,

    /// <summary>Russian.</summary>
    [EnumMember(Value = "russian")]
    Russian,

    /// <summary>Spanish (Spain).</summary>
    [EnumMember(Value = "spanish")]
    Spanish,

    /// <summary>Spanish (Latin America).</summary>
    [EnumMember(Value = "latam")]
    Latam,

    /// <summary>Swedish.</summary>
    [EnumMember(Value = "swedish")]
    Swedish,

    /// <summary>Thai.</summary>
    [EnumMember(Value = "thai")]
    Thai,

    /// <summary>Turkish.</summary>
    [EnumMember(Value = "turkish")]
    Turkish,

    /// <summary>Ukrainian.</summary>
    [EnumMember(Value = "ukrainian")]
    Ukrainian,

    /// <summary>Vietnamese.</summary>
    [EnumMember(Value = "vietnamese")]
    Vietnamese,
}
