using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Items;

/// <summary>A field to group distinct values by for <see cref="SteamWebApiClient.GetItemsPreviewGroupValuesAsync"/>.</summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<PreviewGroupBy>))]
public enum PreviewGroupBy
{
    /// <summary>Group by weapon/item type slug, e.g. "ak-47".</summary>
    [EnumMember(Value = "itemtype")]
    ItemType = 0,

    /// <summary>Group by skin name, e.g. "redline".</summary>
    [EnumMember(Value = "itemname")]
    ItemName,

    /// <summary>Group by rarity label, e.g. "Covert".</summary>
    [EnumMember(Value = "rarity")]
    Rarity,

    /// <summary>Group by collection name.</summary>
    [EnumMember(Value = "collection")]
    Collection,

    /// <summary>Group by exterior/wear.</summary>
    [EnumMember(Value = "wear")]
    Wear,

    /// <summary>Group by Doppler-style phase.</summary>
    [EnumMember(Value = "phase")]
    Phase,

    /// <summary>Group by weapon definition index.</summary>
    [EnumMember(Value = "defindex")]
    DefIndex,

    /// <summary>Group by skin paint index.</summary>
    [EnumMember(Value = "paintindex")]
    PaintIndex,

    /// <summary>Group by full market hash name.</summary>
    [EnumMember(Value = "markethashname")]
    MarketHashName,

    /// <summary>Group by the base display name shared across wear/StatTrak variants.</summary>
    [EnumMember(Value = "groupname")]
    GroupName,
}
